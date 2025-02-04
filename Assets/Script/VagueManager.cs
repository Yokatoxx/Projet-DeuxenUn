// VagueManager.cs
using UnityEngine;

public class VagueManager : MonoBehaviour
{
    [SerializeField] private EnnemySpawn enemySpawn;
    [SerializeField] private Timer timer;
    [SerializeField] private int maxEnemiesInScene = 10;

    private void Start()
    {
        if (enemySpawn == null)
        {
            Debug.LogError("EnnemySpawn n'est pas assigné dans l'inspecteur.");
        }

        if (timer == null)
        {
            Debug.LogError("Timer n'est pas assigné dans l'inspecteur.");
        }
    }

    private void Update()
    {
        if (timer != null)
        {
            int remainingTime = timer.GetCurrentTime();
            float spawnInterval = CalculateSpawnInterval(remainingTime);
            enemySpawn.SetSpawnInterval(spawnInterval);
        }

        int currentEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (currentEnemies < maxEnemiesInScene)
        {
            enemySpawn.EnableSpawning(true);
        }
        else
        {
            enemySpawn.EnableSpawning(false);
        }
    }

    private float CalculateSpawnInterval(int remainingTime)
    {
        // Fonction logarithmique pour ajuster l'intervalle de spawn
        return Mathf.Max(0.5f, Mathf.Log(remainingTime + 1) / 2);
    }
}
