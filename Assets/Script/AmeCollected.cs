// AmeCollected.cs
using UnityEngine;
using System;

public class Ame : MonoBehaviour, ICollectible
{
    public static event Action OnAmeCollected;
    private bool isCollected = false; // Flag pour empêcher les collectes multiples

    public void Collect()
    {
        if (isCollected) return; // Si déjà collectée, ne rien faire
        isCollected = true;
        OnAmeCollected?.Invoke();
        Debug.Log("Ame collected!");
        Destroy(gameObject);
    }

    // Exemple d'implémentation de la collecte via une collision
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collect();
        }
    }
}
