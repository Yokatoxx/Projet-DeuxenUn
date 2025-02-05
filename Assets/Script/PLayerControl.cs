// PLayerControl.cs
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PLayerControl : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody rb;
    public int health = 10;

    public float invincibilityDuration = 2f;
    private float invincibilityTimer = 0f;
    private bool isInvincible = false;

    public float blinkInterval = 0.1f;
    private SpriteRenderer playerSpriteRenderer;

    // Variables pour la régénération de santé
    [SerializeField] private int regenAmount = 2;
    public float currentRegenInterval = 5f; // Intervalle actuel en secondes
    private float regenTimer = 0f;
    private const float minRegenInterval = 1f; // Intervalle minimum en secondes

    // Référence au Canvas GameOver
    [SerializeField] private GameObject gameOverCanvas;

    // Référence à NombreDeVague pour gérer le Game Over
    [SerializeField] private NombreDeVague nombreDeVague;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;

        playerSpriteRenderer = GetComponent<SpriteRenderer>();

        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false);
        }
        else
        {
            Debug.LogWarning("GameOverCanvas n'est pas assigné dans l'inspecteur.");
        }

        if (nombreDeVague == null)
        {
            Debug.LogError("NombreDeVague n'est pas assigné dans l'inspecteur.");
        }
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveX, 0, moveZ) * speed;
        rb.linearVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.z);

        // Flip du sprite avec A et D
        if (Input.GetKey(KeyCode.D))
        {
            playerSpriteRenderer.flipX = false;
            playerSpriteRenderer.transform.localScale = new Vector3(2, 2, 2);
        }
        if (Input.GetKey(KeyCode.A))
        {
            playerSpriteRenderer.flipX = true;
            playerSpriteRenderer.transform.localScale = new Vector3(2, 2, 2);
        }

        // Gestion du timer d'invincibilité
        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;
            if (invincibilityTimer <= 0f)
            {
                isInvincible = false;
                if (playerSpriteRenderer != null)
                {
                    playerSpriteRenderer.enabled = true;
                }
            }
        }

        // Gestion de la régénération de santé
        if (health < 10) // Supposons que 10 est la santé maximale
        {
            regenTimer += Time.deltaTime;
            if (regenTimer >= currentRegenInterval)
            {
                RegenererSante();
                regenTimer = 0f;
            }
        }

        // Vérification de la santé du joueur pour le Game Over
        if (health <= 0f)
        {
            HandleGameOver();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            DimensionManager.Instance.SwitchDimension();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isInvincible)
        {
            PrendreDegats(1);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isInvincible)
        {
            PrendreDegats(1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BalleBlancheEnemy") && !isInvincible)
        {
            PrendreDegats(1);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("BalleBlancheEnemy") && !isInvincible)
        {
            PrendreDegats(1);
        }
    }

    private void PrendreDegats(int montant)
    {
        health -= montant;
        Debug.Log("Health: " + health);
        isInvincible = true;
        invincibilityTimer = invincibilityDuration;
        StartCoroutine(Clignoter());
    }

    private IEnumerator Clignoter()
    {
        if (playerSpriteRenderer == null)
            yield break;

        float elapsed = 0f;
        while (elapsed < invincibilityDuration)
        {
            playerSpriteRenderer.enabled = !playerSpriteRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval;
        }
        playerSpriteRenderer.enabled = true;
    }

    private void RegenererSante()
    {
        health += regenAmount;
        if (health > 10)
        {
            health = 10;
        }
        Debug.Log("Santé régénérée. Santé actuelle : " + health);
    }

    public void ReduireIntervalleRegen(float reduction)
    {
        currentRegenInterval = Mathf.Max(minRegenInterval, currentRegenInterval - reduction);
    }

    // Nouvelle méthode pour gérer le Game Over
    private void HandleGameOver()
    {
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
            Debug.Log("Game Over! Affichage du Canvas Game Over.");

            // Désactiver le contrôle du joueur
            this.enabled = false;
        }
        else
        {
            Debug.LogWarning("GameOverCanvas n'est pas assigné.");
        }

        if (nombreDeVague != null)
        {
            nombreDeVague.HandleGameOver();
        }
        else
        {
            Debug.LogError("NombreDeVague n'est pas assigné. Impossible de gérer le Game Over.");
        }
    }
}
