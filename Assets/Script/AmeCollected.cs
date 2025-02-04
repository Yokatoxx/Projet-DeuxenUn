using UnityEngine;
using System;

public class Ame : MonoBehaviour, ICollectible
{
    public static event Action OnAmeCollected;
    public void Collect()
    {
        OnAmeCollected?.Invoke();
        Debug.Log("Ame collected!");
        Destroy(gameObject);
    }
}

