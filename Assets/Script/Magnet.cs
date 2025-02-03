using UnityEngine;

public class Magnet : MonoBehaviour

{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Actractor attractable = collision.GetComponent<Actractor>();

        if (attractable != null)
        {
            attractable.target = transform;
        }
    }
}
