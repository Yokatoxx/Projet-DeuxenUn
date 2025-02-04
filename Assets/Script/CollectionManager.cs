// CollectionManager.cs
using UnityEngine;
using TMPro;

public class CollectionManager : MonoBehaviour
{
    public TextMeshProUGUI ameUI;
    int ameCollected = 0;

    private void OnEnable()
    {
        Ame.OnAmeCollected += AmeCollected;
        if (Economy.Instance != null)
        {
            Economy.Instance.OnCoinsChanged += UpdateAmeUI;
            UpdateAmeUI(Economy.Instance.Coins);
        }
    }

    private void OnDisable()
    {
        Ame.OnAmeCollected -= AmeCollected;
        if (Economy.Instance != null)
        {
            Economy.Instance.OnCoinsChanged -= UpdateAmeUI;
        }
    }

    private void AmeCollected()
    {
        ameCollected++;
        // La mise à jour de l'UI est désormais gérée par l'événement OnCoinsChanged
    }

    private void UpdateAmeUI(int currentCoins)
    {
        ameUI.text = $"{currentCoins}";
    }
}
