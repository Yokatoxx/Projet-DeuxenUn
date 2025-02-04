using UnityEngine;

public class Actractor : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 2f;

    private void Update()
    {
        if (target)
        {
            Vector3 direction = target.position - transform.position; // Utiliser Vector3
            transform.Translate(direction.normalized * moveSpeed * Time.deltaTime, Space.World);
        }
    }
}