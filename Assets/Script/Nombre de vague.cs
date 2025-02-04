// NombreDeVague.cs
using UnityEngine;

public class NombreDeVague : MonoBehaviour
{
    [SerializeField] private VagueManager vagueManager;
    [SerializeField] private Timer timer;
    [SerializeField] private Canvas menuAmelioration;

    private int numeroVague = 1;
    private int maxVagues = 10;

    private void Start()
    {
        if (vagueManager == null)
        {
            Debug.LogError("VagueManager n'est pas assign� dans l'inspecteur.");
        }

        if (timer == null)
        {
            Debug.LogError("Timer n'est pas assign� dans l'inspecteur.");
        }

        if (menuAmelioration == null)
        {
            Debug.LogError("MenuAmelioration n'est pas assign� dans l'inspecteur.");
        }

        timer.OnTimerFinished += FinDeVague;
        DemarrerVague();
    }

    private void DemarrerVague()
    {
        // Calcul du temps de la vague en fonction du num�ro de vague
        int newTime = Mathf.Min(15 + numeroVague * 5, 60);
        timer.ResetTimer(newTime);

        // Mise � jour du nombre maximal d'ennemis
        int maxEnnemis = 5 + numeroVague * 2;
        vagueManager.SetMaxEnemiesInScene(maxEnnemis);

        // D�marrer le spawn des ennemis
        vagueManager.StartSpawning();

        // Cacher le menu d'am�lioration
        menuAmelioration.gameObject.SetActive(false);
    }

    private void FinDeVague()
    {
        // Afficher le menu d'am�lioration
        menuAmelioration.gameObject.SetActive(true);

        // Arr�ter le spawn des ennemis
        vagueManager.StopSpawning();

        // D�truire tous les ennemis restants
        timer.DestroyAllEnnemies();
    }

    // Cette m�thode doit �tre appel�e lorsque le joueur a fait son choix d'am�lioration
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
            Debug.Log("Toutes les vagues ont �t� compl�t�es !");
        }
    }
}
