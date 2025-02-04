// NombreDeVague.cs
using UnityEngine;

public class NombreDeVague : MonoBehaviour
{
    [SerializeField] private VagueManager vagueManager;
    [SerializeField] private Timer timer;
    [SerializeField] private Canvas menuAmelioration;

    public int numeroVague = 1;
    private int maxVagues = 10;

    private void Start()
    {
        if (vagueManager == null)
        {
            Debug.LogError("VagueManager n'est pas assigné dans l'inspecteur.");
        }

        if (timer == null)
        {
            Debug.LogError("Timer n'est pas assigné dans l'inspecteur.");
        }

        if (menuAmelioration == null)
        {
            Debug.LogError("MenuAmelioration n'est pas assigné dans l'inspecteur.");
        }

        timer.OnTimerFinished += FinDeVague;
        DemarrerVague();
    }

    private void DemarrerVague()
    {
        // Calcul du temps de la vague en fonction du numéro de vague
        int newTime = Mathf.Min(15 + numeroVague * 5, 60);
        timer.ResetTimer(newTime);

        // Mise à jour du nombre maximal d'ennemis
        int maxEnnemis = 5 + numeroVague * 2;
        vagueManager.SetMaxEnemiesInScene(maxEnnemis);

        // Démarrer le spawn des ennemis
        vagueManager.StartSpawning();

        // Cacher le menu d'amélioration
        menuAmelioration.gameObject.SetActive(false);
    }

    private void FinDeVague()
    {
        // Afficher le menu d'amélioration
        menuAmelioration.gameObject.SetActive(true);

        // Arrêter le spawn des ennemis
        vagueManager.StopSpawning();

        // Détruire tous les ennemis restants
        timer.DestroyAllEnnemies();
    }

    // Cette méthode doit être appelée lorsque le joueur a fait son choix d'amélioration
    public void OnAmeliorationChoisie()
    {
        if (numeroVague < maxVagues)
        {
            numeroVague++;
            DemarrerVague();
        }
        else
        {
            // Fin du jeu ou autre action
            Debug.Log("Toutes les vagues ont été complétées !");
        }
    }
}
