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

    // Prix pour chaque am�lioration
    private readonly int speedBonusPrice = 3;
    private readonly int shootingRateBonusPrice = 10;
    private readonly int regenRateBonusPrice = 15;

    // �v�nement d�clench� lorsque les coins changent
    public event Action<int> OnCoinsChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // Optionnel : charger les coins sauvegard�s ici
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
        Debug.Log($"Coins ajout�s. Total actuel: {Coins}");
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
            Debug.Log($"Achat effectu�. Montant d�pens�: {amount}. Coins restants: {Coins}");
            return true;
        }
        Debug.Log("Achat �chou�. Fonds insuffisants.");
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
