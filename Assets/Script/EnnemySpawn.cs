// EnnemySpawn.cs
using UnityEngine;
using System.Collections;

public class EnnemySpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab1;
    [SerializeField]
    private GameObject enemyPrefab2;

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
        // Rotation de 90 degrés sur l'axe X
        Quaternion spawnRotation = Quaternion.Euler(90f, 0f, 0f);
        // Choisir aléatoirement entre les deux prefabs
        GameObject prefabToSpawn = Random.value > 0.5f ? enemyPrefab1 : enemyPrefab2;
        Instantiate(prefabToSpawn, spawnPoints[index].position, spawnRotation);
    }
}
