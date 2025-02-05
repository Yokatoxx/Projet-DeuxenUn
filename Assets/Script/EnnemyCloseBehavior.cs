using System.Collections;
using UnityEngine;

public class EnnemyCloseBehavior : MonoBehaviour
{
    private Transform cible;
    public float vitesse = 5f;
    private Vector3 originalScale;
    public GameObject Drop;
    [Header("Vie")]
    public int maxHealth = 3;
    [SerializeField] private int currentHealth;

    private SpriteRenderer spriteRenderer;
    private bool isBlinking = false;

    void Start()
    {
        currentHealth = maxHealth;
        originalScale = transform.localScale;
        spriteRenderer = GetComponent<SpriteRenderer>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            cible = player.transform;
        }
    }

    void Update()
    {
        if (cible != null)
        {
            Vector3 direction = (cible.position - transform.position).normalized;
            transform.position += direction * vitesse * Time.deltaTime;

            // Flip sprite based on X movement direction
            if (direction.x > 0)
            {
                // Face right (original scale)
                transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
            }
            else if (direction.x < 0)
            {
                // Face left (flipped X scale)
                transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("BalleNoir"))
        {
            PrendreDegats(1);
            Destroy(collision.gameObject);
        }
    }

    public void PrendreDegats(int degats)
    {
        currentHealth -= degats;

        if (!isBlinking)
        {
            StartCoroutine(Blink());
        }

        if (currentHealth <= 0)
        {
            Mourir();
        }
    }

    private IEnumerator Blink()
    {
        isBlinking = true;
        int blinkCount = 6;
        float blinkDuration = 0.1f;

        for (int i = 0; i < blinkCount; i++)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(blinkDuration);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(blinkDuration);
        }

        isBlinking = false;
    }

    private void Mourir()
    {
        Instantiate(Drop, transform.position, Quaternion.Euler(90, 0, 0));
        Destroy(gameObject);
    }
}
