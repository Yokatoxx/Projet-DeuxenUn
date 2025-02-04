using UnityEngine;

public class Magnet : MonoBehaviour

{

    private void OnTriggerEnter(Collider collision)
    {
        Actractor attractable = collision.GetComponent<Actractor>();

        if (attractable != null)
        {
            attractable.target = transform;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        Actractor attractable = collision.GetComponent<Actractor>();

        if (attractable != null)
        {
            attractable.target = null;
        }
    }
}
