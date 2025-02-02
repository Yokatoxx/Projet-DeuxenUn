using UnityEngine;

public class EnnemyCloseBehavior : MonoBehaviour
{
    private Transform cible;
    public float vitesse = 5f;

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
            Vector3 direction = (cible.position - transform.position).normalized;
            transform.position += direction * vitesse * Time.deltaTime;
        }
    }
}
