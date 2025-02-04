// CollectionManager.cs
using UnityEngine;
using TMPro;

public class CollectionManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ameUI;

    private void OnEnable()
    {
        if (Economy.Instance != null)
        {
            Economy.Instance.OnCoinsChanged += UpdateAmeUI;
            UpdateAmeUI(Economy.Instance.Coins);
        }
        else
        {
            Debug.LogError("Instance d'Economy non trouvée.");
        }
    }

    private void OnDisable()
    {
        if (Economy.Instance != null)
        {
            Economy.Instance.OnCoinsChanged -= UpdateAmeUI;
        }
    }

    private void UpdateAmeUI(int currentCoins)
    {
        if (ameUI != null)
        {
            ameUI.text = $"{currentCoins} Ame";
        }
        else
        {
            Debug.LogError("ameUI n'est pas assigné dans l'inspecteur.");
        }
    }
}
