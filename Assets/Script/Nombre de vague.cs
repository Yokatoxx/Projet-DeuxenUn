// NombreDeVague.cs
using UnityEngine;
using System;

public class NombreDeVague : MonoBehaviour
{
    [SerializeField] private VagueManager vagueManager;
    [SerializeField] private Timer timer;
    [SerializeField] private Canvas menuAmelioration;

    public int numeroVague = 1;
    private int maxVagues = 10;

    public int NumeroVague => numeroVague;
    [SerializeField] private Canvas menuVictory;

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
        int newTime = Mathf.Min(15 + numeroVague * 5, 60);
        timer.ResetTimer(newTime);

        int maxEnnemis = 5 + numeroVague * 2;
        vagueManager.SetMaxEnemiesInScene(maxEnnemis);

        vagueManager.StartSpawning();

        menuAmelioration.gameObject.SetActive(false);

        Debug.Log($"Début de la Vague {numeroVague}");
    }

    private void FinDeVague()
    {
        menuAmelioration.gameObject.SetActive(true);

        vagueManager.StopSpawning();

        timer.DestroyAllEnnemies();
    }

    public void OnAmeliorationChoisie()
    {
        if (numeroVague < maxVagues)
        {
            numeroVague++;
            DemarrerVague();
        }
        else
        {
            menuVictory.enabled = true;
            Debug.Log("Toutes les vagues ont été complétées !");
        }
    }

    public void HandleGameOver()
    {
        Debug.Log("Jeu terminé. Affichage du Game Over.");

        vagueManager.StopSpawning();

        timer.DestroyAllEnnemies();

        if (menuAmelioration != null)
        {
            menuAmelioration.gameObject.SetActive(false);
        }
    }
}
