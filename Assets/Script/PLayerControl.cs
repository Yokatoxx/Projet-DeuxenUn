using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class PLayerControl : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody rb;
    private int health = 10;

    public float invincibilityDuration = 2f;
    private float invincibilityTimer = 0f;
    private bool isInvincible = false;

    public float blinkInterval = 0.1f;
    private Renderer playerRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;

        playerRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveX, 0, moveZ) * speed;
        rb.linearVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.z);

        // Gestion du timer d'invincibilité
        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;
            if (invincibilityTimer <= 0f)
            {
                isInvincible = false;
                if (playerRenderer != null)
                {
                    playerRenderer.enabled = true;
                }
            }
        }
        if (health <= 0f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);



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
        if (other.gameObject.CompareTag("Balle") && !isInvincible)
        {
            PrendreDegats(1);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Balle") && !isInvincible)
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
        if (playerRenderer == null)
            yield break;

        float elapsed = 0f;
        while (elapsed < invincibilityDuration)
        {
            playerRenderer.enabled = !playerRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval;
        }
        playerRenderer.enabled = true;
    }
    
}
