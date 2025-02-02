using UnityEngine;
using TMPro;
using System.Collections;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    private int currentTime = 60; // Exemple de temps initial

    private void Start()
    {
        UpdateTimerText();
        StartCoroutine(DecrementTimer());
    }

    private IEnumerator DecrementTimer()
    {
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1f);
            currentTime--;
            UpdateTimerText();
        }
    }

    private void UpdateTimerText()
    {
        timerText.text = currentTime.ToString();
    }
}
