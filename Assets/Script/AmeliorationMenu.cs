// AmeliorationMenu.cs
using UnityEngine;

public class AmeliorationMenu : MonoBehaviour
{
    [SerializeField] private NombreDeVague nombreDeVague;
    [SerializeField] private Timer timer;
    [SerializeField] private GameObject spawner;
    [SerializeField] private PLayerControl player;
    [SerializeField] private Shoot hand;

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
    }

    public void SpeedBonus()
    {
        if (player != null)
        {
            player.speed += 1;
            Debug.Log("Bonus de vitesse appliqu�. Nouvelle vitesse : " + player.speed);
        }
        OnAmeliorationChoisie();
    }

    public void ShootingRateBonus()
    {
        if (hand != null)
        {
            hand.shootCooldown = Mathf.Max(0.1f, hand.shootCooldown - 0.1f); // Diminue le cooldown pour augmenter le taux de tir
            Debug.Log("Bonus de taux de tir appliqu�. Nouveau cooldown : " + hand.shootCooldown);
        }
        OnAmeliorationChoisie();
    }

    public void HealingBonus()
    {
        if (player != null)
        {
            player.health += 5; // Exemple : augmente la sant� du joueur
            Debug.Log("Bonus de sant� appliqu�. Nouvelle sant� : " + player.health);
        }
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
