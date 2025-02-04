// Timer.cs
using UnityEngine;
using TMPro;
using System.Collections;
using System;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    public int currentTime = 15; // Exemple de temps initial

    public Canvas menuAmelioration;
    public GameObject spawnManager;

    // Déclaration de l'événement pour la fin du timer
    public event Action OnTimerFinished;

    private Coroutine decrementCoroutine;

    private void Start()
    {
        UpdateTimerText();
        // Suppression du démarrage automatique du timer
    }

    public IEnumerator DecrementTimer()
    {
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1f);
            currentTime--;
            UpdateTimerText();
        }

        // Notifier que le timer est terminé
        OnTimerFinished?.Invoke();
    }

    public void UpdateTimerText()
    {
        timerText.text = currentTime.ToString();
    }

    public int GetCurrentTime()
    {
        return currentTime;
    }

    public void ResetTimer(int newTime)
    {
        if (decrementCoroutine != null)
        {
            StopCoroutine(decrementCoroutine);
        }
        currentTime = newTime;
        UpdateTimerText();
        decrementCoroutine = StartCoroutine(DecrementTimer());
    }

    public void DestroyAllEnnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }
}
