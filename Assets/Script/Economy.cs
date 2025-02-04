// Economy.cs
using UnityEngine;
using System;

public class Economy : MonoBehaviour
{
    public static Economy Instance { get; private set; }

    private int coins = 0;
    public int Coins
    {
        get => coins;
        private set
        {
            coins = value;
            OnCoinsChanged?.Invoke(coins);
        }
    }

    // Prix pour chaque amélioration
    private readonly int speedBonusPrice = 3;
    private readonly int shootingRateBonusPrice = 10;
    private readonly int regenRateBonusPrice = 15;

    // Événement déclenché lorsque les coins changent
    public event Action<int> OnCoinsChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // Optionnel : charger les coins sauvegardés ici
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        Ame.OnAmeCollected += AddCoins;
    }

    private void OnDisable()
    {
        Ame.OnAmeCollected -= AddCoins;
    }

    private void AddCoins()
    {
        Coins += 1; // Supposant que chaque Ame ajoute 1 coin
        Debug.Log($"Coins ajoutés. Total actuel: {Coins}");
    }

    public bool CanAfford(int amount)
    {
        return Coins >= amount;
    }

    public bool Purchase(int amount)
    {
        if (CanAfford(amount))
        {
            Coins -= amount;
            Debug.Log($"Achat effectué. Montant dépensé: {amount}. Coins restants: {Coins}");
            return true;
        }
        Debug.Log("Achat échoué. Fonds insuffisants.");
        return false;
    }

    public int GetPrice(string upgradeType)
    {
        return upgradeType switch
        {
            "SpeedBonus" => speedBonusPrice,
            "ShootingRateBonus" => shootingRateBonusPrice,
            "RegenRateBonus" => regenRateBonusPrice,
            _ => 0,
        };
    }
}
