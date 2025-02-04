using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPlay: MonoBehaviour
{

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("BalleBlanche"))
        {
            Destroy(collision.gameObject);
            SceneManager.LoadScene(0);
        }

    }
}
