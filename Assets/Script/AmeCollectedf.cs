using UnityEngine;

public class NewEmptyCSharpScript : MonoBehaviour, ICollectible
{
    public void Collect()
    {
        Debug.Log("Ame collected!");
        Destroy(gameObject);
    }
}

