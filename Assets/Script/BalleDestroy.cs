using UnityEngine;

public class BalleDestroy : MonoBehaviour
{
    [SerializeField] private float tempsAvantDestruction = 5f;

    void Start()
    {
        Destroy(gameObject, tempsAvantDestruction);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
