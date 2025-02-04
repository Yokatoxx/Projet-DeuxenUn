// EnnemySpawn.cs
using UnityEngine;
using System.Collections;

public class EnnemySpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private float spawnInterval = 2f;

    [SerializeField]
    private float spawnDuration = 10f;

    [SerializeField]
    private Transform[] spawnPoints;

    private bool isSpawning = true;
    private Coroutine spawnCoroutine;

    private void Start()
    {
        spawnCoroutine = StartCoroutine(SpawnEnemies());
    }

    public void SetSpawnInterval(float interval)
    {
        spawnInterval = interval;
    }

    public void EnableSpawning(bool enable)
    {
        if (enable && !isSpawning)
        {
            isSpawning = true;
            spawnCoroutine = StartCoroutine(SpawnEnemies());
        }
        else if (!enable && isSpawning)
        {
            isSpawning = false;
            if (spawnCoroutine != null)
            {
                StopCoroutine(spawnCoroutine);
            }
        }
    }

    private IEnumerator SpawnEnemies()
    {
        float elapsed = 0f;
        isSpawning = true;
        while (elapsed < spawnDuration && isSpawning)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
            elapsed += spawnInterval;
        }
        isSpawning = false;
    }

    private void SpawnEnemy()
    {
        int index = Random.Range(0, spawnPoints.Length);
        // Rotation modifiée avec 90 degrés sur l'axe X
        Quaternion spawnRotation = Quaternion.Euler(90f, 0f, 0f);
        Instantiate(enemyPrefab, spawnPoints[index].position, spawnRotation);
    }
}
