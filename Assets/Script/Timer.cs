// Timer.cs
using UnityEngine;
using TMPro;
using System.Collections;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    public int currentTime = 15; // Exemple de temps initial

    public Canvas menuAmelioration;
    public GameObject spawnManager;

    private void Start()
    {
        UpdateTimerText();
        StartCoroutine(DecrementTimer());
    }

    public IEnumerator DecrementTimer()
    {
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1f);
            currentTime--;
            UpdateTimerText();
        }

        menuAmelioration.gameObject.SetActive(true);
        spawnManager.SetActive(false);
        DestroyAllEnnemies();
    }

    public void UpdateTimerText()
    {
        timerText.text = currentTime.ToString();
    }

    public int GetCurrentTime()
    {
        return currentTime;
    }

    private void DestroyAllEnnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); 
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }
}
