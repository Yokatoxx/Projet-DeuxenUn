// AmeliorationMenu.cs
using UnityEngine;
using TMPro;

public class AmeliorationMenu : MonoBehaviour
{
    [SerializeField] private NombreDeVague nombreDeVague;
    [SerializeField] private Timer timer;
    [SerializeField] private GameObject spawner;
    [SerializeField] private PLayerControl player;
    [SerializeField] private Shoot hand;

    // Éléments TextMeshProUGUI pour afficher les prix des améliorations
    [SerializeField] private TextMeshProUGUI speedBonusPriceText;
    [SerializeField] private TextMeshProUGUI shootingRateBonusPriceText;
    [SerializeField] private TextMeshProUGUI regenRateBonusPriceText;

    private void Start()
    {
        if (nombreDeVague == null)
        {
            Debug.LogError("NombreDeVague n'est pas assigné dans l'inspecteur.");
        }

        if (timer == null)
        {
            Debug.LogError("Timer n'est pas assigné dans l'inspecteur.");
        }

        if (spawner == null)
        {
            Debug.LogError("Spawner n'est pas assigné dans l'inspecteur.");
        }

        if (player == null)
        {
            Debug.LogError("Player n'est pas assigné dans l'inspecteur.");
        }

        if (hand == null)
        {
            Debug.LogError("Hand (Shoot) n'est pas assigné dans l'inspecteur.");
        }

        // Initialiser les textes des prix des améliorations
        InitializeUpgradePrices();
    }

    private void InitializeUpgradePrices()
    {
        if (Economy.Instance == null)
        {
            Debug.LogError("Instance d'Economy non trouvée.");
            return;
        }

        speedBonusPriceText.text = $"{Economy.Instance.GetPrice("SpeedBonus")}";
        shootingRateBonusPriceText.text = $"{Economy.Instance.GetPrice("ShootingRateBonus")}";
        regenRateBonusPriceText.text = $"{Economy.Instance.GetPrice("RegenRateBonus")}";
    }

    public void SpeedBonus()
    {
        int price = Economy.Instance.GetPrice("SpeedBonus");
        if (Economy.Instance.Purchase(price))
        {
            if (player != null)
            {
                player.speed += 1;
                Debug.Log("Bonus de vitesse appliqué. Nouvelle vitesse : " + player.speed);
            }
            OnAmeliorationChoisie();
        }
        else
        {
            Debug.Log("Pas assez d'Ame pour le bonus de vitesse.");
        }
    }

    public void ShootingRateBonus()
    {
        int price = Economy.Instance.GetPrice("ShootingRateBonus");
        if (Economy.Instance.Purchase(price))
        {
            if (hand != null)
            {
                hand.shootCooldown = Mathf.Max(0.1f, hand.shootCooldown - 0.1f);
                Debug.Log("Bonus de taux de tir appliqué. Nouveau cooldown : " + hand.shootCooldown);
            }
            OnAmeliorationChoisie();
        }
        else
        {
            Debug.Log("Pas assez d'Ame pour le bonus de taux de tir.");
        }
    }

    public void RegenRateBonus()
    {
        int price = Economy.Instance.GetPrice("RegenRateBonus");
        if (Economy.Instance.Purchase(price))
        {
            if (player != null)
            {
                player.ReduireIntervalleRegen(0.5f); // Réduit l'intervalle de régénération de 0.5 secondes
                Debug.Log("Bonus de régénération appliqué. Nouveau intervalle de régénération : " + player.currentRegenInterval + " secondes");
            }
            OnAmeliorationChoisie();
        }
        else
        {
            Debug.Log("Pas assez d'Ame pour le bonus de régénération.");
        }
    }
    public void CloseMenu()
    {
        Debug.Log("Menu d'amélioration fermé sans sélection d'amélioration.");
        OnAmeliorationChoisie();
    }
    private void OnAmeliorationChoisie()
    {
        if (nombreDeVague != null)
        {
            nombreDeVague.OnAmeliorationChoisie();
        }
        else
        {
            Debug.LogError("NombreDeVague n'est pas assigné. Impossible de démarrer la prochaine vague.");
        }

        // Désactiver le menu d'amélioration
        gameObject.SetActive(false);
    }
}
