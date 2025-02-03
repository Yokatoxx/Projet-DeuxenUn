using UnityEngine;

public class Actractor : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 1f;

    private void Update()
    {
        if (target)
        {
            Vector2 direction = target.position - transform.position;
            transform.Translate(direction.normalized * moveSpeed * Time.deltaTime);



        }
    }
}
