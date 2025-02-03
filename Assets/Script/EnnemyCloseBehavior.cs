using UnityEngine;

public class EnnemyCloseBehavior : MonoBehaviour
{
    private Transform cible;
    public float vitesse = 5f;

    [Header("Vie")]
    public int maxHealth = 3;
    [SerializeField] private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;

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
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Balle"))
        {
            PrendreDegats(1);
            Destroy(collision.gameObject);
        }
    }

    public void PrendreDegats(int degats)
    {
        currentHealth -= degats;

        if (currentHealth <= 0)
        {
            Mourir();
        }
    }

    private void Mourir()
    {
        
        Destroy(gameObject);
    }
}