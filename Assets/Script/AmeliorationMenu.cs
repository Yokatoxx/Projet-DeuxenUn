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

    // �l�ments TextMeshProUGUI pour afficher les prix des am�liorations
    [SerializeField] private TextMeshProUGUI speedBonusPriceText;
    [SerializeField] private TextMeshProUGUI shootingRateBonusPriceText;
    [SerializeField] private TextMeshProUGUI regenRateBonusPriceText;

    private void Start()
    {
        if (nombreDeVague == null)
        {
            Debug.LogError("NombreDeVague n'est pas assign� dans l'inspecteur.");
        }

        if (timer == null)
        {
            Debug.LogError("Timer n'est pas assign� dans l'inspecteur.");
        }

        if (spawner == null)
        {
            Debug.LogError("Spawner n'est pas assign� dans l'inspecteur.");
        }

        if (player == null)
        {
            Debug.LogError("Player n'est pas assign� dans l'inspecteur.");
        }

        if (hand == null)
        {
            Debug.LogError("Hand (Shoot) n'est pas assign� dans l'inspecteur.");
        }

        // Initialiser les textes des prix des am�liorations
        InitializeUpgradePrices();
    }

    private void InitializeUpgradePrices()
    {
        if (Economy.Instance == null)
        {
            Debug.LogError("Instance d'Economy non trouv�e.");
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
                Debug.Log("Bonus de vitesse appliqu�. Nouvelle vitesse : " + player.speed);
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
                Debug.Log("Bonus de taux de tir appliqu�. Nouveau cooldown : " + hand.shootCooldown);
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
                player.ReduireIntervalleRegen(0.5f); // R�duit l'intervalle de r�g�n�ration de 0.5 secondes
                Debug.Log("Bonus de r�g�n�ration appliqu�. Nouveau intervalle de r�g�n�ration : " + player.currentRegenInterval + " secondes");
            }
            OnAmeliorationChoisie();
        }
        else
        {
            Debug.Log("Pas assez d'Ame pour le bonus de r�g�n�ration.");
        }
    }
    public void CloseMenu()
    {
        Debug.Log("Menu d'am�lioration ferm� sans s�lection d'am�lioration.");
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
            Debug.LogError("NombreDeVague n'est pas assign�. Impossible de d�marrer la prochaine vague.");
        }

        // D�sactiver le menu d'am�lioration
        gameObject.SetActive(false);
    }
}
