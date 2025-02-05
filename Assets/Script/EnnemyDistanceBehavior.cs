using System.Collections;
using UnityEngine;

public class EnnemyDistanceBehavior : MonoBehaviour
{
    private Transform cible;
    public float vitesse = 5f;
    public float distanceDeTir = 10f;
    public float distanceMin = 5f;
    public GameObject projectilePrefab;
    public float tauxDeTir = 1f;
    private float prochainTir;
    public float vitesseProjectile = 20f;
    public float offsetSpawn = 1.5f;
    private Vector3 originalScale; // Stockage de l'�chelle originale
    [SerializeField] private int currentHealth;
    public int maxHealth = 3;
    public GameObject Drop;

    private SpriteRenderer spriteRenderer;
    private bool isBlinking = false;

    void Start()
    {
        originalScale = transform.localScale; // Initialiser l'�chelle originale

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            cible = player.transform;
        }
        currentHealth = maxHealth;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (cible != null)
        {
            float distance = Vector3.Distance(transform.position, cible.position);
            Vector3 direction;

            if (distance > distanceMin)
            {
                // D�placement vers le joueur
                direction = (cible.position - transform.position).normalized;
                transform.position += direction * vitesse * Time.deltaTime;

                // Gestion du flip
                if (direction.x > 0) transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
                else if (direction.x < 0) transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);

                // Logique de tir
                if (distance <= distanceDeTir && Time.time >= prochainTir)
                {
                    Tirer();
                    prochainTir = Time.time + 1f / tauxDeTir;
                }
            }
            else
            {
                // D�placement inverse pour garder la distance
                direction = (transform.position - cible.position).normalized;
                transform.position += direction * vitesse * Time.deltaTime;

                // Gestion du flip (invers�e car direction oppos�e)
                if (direction.x > 0) transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
                else if (direction.x < 0) transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
            }
        }
    }

    void Tirer()
    {
        if (cible == null) return;

        Vector3 directionProjectile = (cible.position - transform.position).normalized;
        Vector3 spawnPosition = transform.position + directionProjectile * offsetSpawn;

        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.Euler(90, 0, 0));
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = directionProjectile * vitesseProjectile;
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("BalleBlanche"))
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
