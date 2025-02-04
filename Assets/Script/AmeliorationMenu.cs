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
    }

    public void SpeedBonus()
    {
        if (player != null)
        {
            player.speed += 1;
            Debug.Log("Bonus de vitesse appliqué. Nouvelle vitesse : " + player.speed);
        }
        OnAmeliorationChoisie();
    }

    public void ShootingRateBonus()
    {
        if (hand != null)
        {
            hand.shootCooldown = Mathf.Max(0.1f, hand.shootCooldown - 0.1f); // Diminue le cooldown pour augmenter le taux de tir
            Debug.Log("Bonus de taux de tir appliqué. Nouveau cooldown : " + hand.shootCooldown);
        }
        OnAmeliorationChoisie();
    }

    public void HealingBonus()
    {
        if (player != null)
        {
            player.health += 5; // Exemple : augmente la santé du joueur
            Debug.Log("Bonus de santé appliqué. Nouvelle santé : " + player.health);
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
            Debug.LogError("NombreDeVague n'est pas assigné. Impossible de démarrer la prochaine vague.");
        }

        // Désactiver le menu d'amélioration
        gameObject.SetActive(false);
    }
}
