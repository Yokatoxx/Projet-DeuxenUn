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

    void Start()
    {
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
            float distance = Vector3.Distance(transform.position, cible.position);
            Vector3 direction;

            if (distance > distanceMin)
            {
                direction = (cible.position - transform.position).normalized;
                transform.position += direction * vitesse * Time.deltaTime;

                if (distance <= distanceDeTir)
                {
                    if (Time.time >= prochainTir)
                    {
                        Tirer();
                        prochainTir = Time.time + 1f / tauxDeTir;
                    }
                }
            }
            else
            {
                direction = (transform.position - cible.position).normalized;
                transform.position += direction * vitesse * Time.deltaTime;
            }
        }
    }

    void Tirer()
    {
        if (cible == null) return;

        Vector3 directionProjectile = (cible.position - transform.position).normalized;
        Vector3 spawnPosition = transform.position + directionProjectile * offsetSpawn;

        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.LookRotation(directionProjectile));
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = directionProjectile * vitesseProjectile;
        }
    }
}
