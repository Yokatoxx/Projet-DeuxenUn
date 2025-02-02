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

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        float elapsed = 0f;
        while (elapsed < spawnDuration)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
            elapsed += spawnInterval;
        }
    }

    private void SpawnEnemy()
    {

        int index = Random.Range(0, spawnPoints.Length);
        Instantiate(enemyPrefab, spawnPoints[index].position, Quaternion.identity);
    }
}
