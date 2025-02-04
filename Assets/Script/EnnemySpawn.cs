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
    private GameObject spawnIndicatorPrefab1;
    [SerializeField]
    private GameObject spawnIndicatorPrefab2;

    [SerializeField]
    private float spawnInterval = 2f;

    [SerializeField]
    private float spawnDuration = 10f;

    [SerializeField]
    private float spawnIndicatorDuration = 1f;

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
            StartCoroutine(SpawnEnemyCoroutine());
            yield return new WaitForSeconds(spawnInterval);
            elapsed += spawnInterval;
        }
        isSpawning = false;
    }

    private IEnumerator SpawnEnemyCoroutine()
    {
        int index = Random.Range(0, spawnPoints.Length);
        Vector3 spawnPosition = spawnPoints[index].position;

        // Rotation de 90 degrés sur l'axe X
        Quaternion spawnRotation = Quaternion.Euler(90f, 0f, 0f);

        // Choisir aléatoirement l'ennemi et son indicateur
        bool spawnFirstEnemy = Random.value > 0.5f;
        GameObject prefabToSpawn = spawnFirstEnemy ? enemyPrefab1 : enemyPrefab2;
        GameObject indicatorPrefab = spawnFirstEnemy ? spawnIndicatorPrefab1 : spawnIndicatorPrefab2;

        // Afficher l'indicateur de spawn correspondant avec rotation
        if (indicatorPrefab != null)
        {
            // Instancier le prefab indicateur avec rotation
            GameObject spawnIndicator = Instantiate(indicatorPrefab, spawnPosition, Quaternion.Euler(90f, 0f, 0f));

            // Attendre la durée de l'indicateur
            yield return new WaitForSeconds(spawnIndicatorDuration);

            // Détruire l'indicateur de spawn
            Destroy(spawnIndicator);
        }

        // Instancier l'ennemi avec rotation
        Instantiate(prefabToSpawn, spawnPosition, spawnRotation);
    }
}
