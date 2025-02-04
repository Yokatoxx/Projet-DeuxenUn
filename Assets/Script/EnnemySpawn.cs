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
    private float spawnIndicatorDuration = 1f;

    [SerializeField]
    private Transform[] spawnPoints;

    private bool isSpawning = false;
    private Coroutine spawnCoroutine;

    private void Start()
    {
        // Ne pas démarrer automatiquement le spawning ici
        // Le contrôle du démarrage est effectué via VagueManager
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
        isSpawning = true;
        while (isSpawning)
        {
            StartCoroutine(SpawnEnemyCoroutine());
            yield return new WaitForSeconds(spawnInterval);
        }
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

        // Afficher l'indicateur de spawn avec rotation
        GameObject spawnIndicator = null;
        if (indicatorPrefab != null)
        {
            // Instancier le prefab indicateur avec rotation
            spawnIndicator = Instantiate(indicatorPrefab, spawnPosition, Quaternion.Euler(90f, 0f, 0f));
        }

        // Attendre la durée de l'indicateur
        float timer = 0f;
        while (timer < spawnIndicatorDuration)
        {
            if (!isSpawning)
            {
                // Si le spawning est désactivé, sortir de la coroutine
                if (spawnIndicator != null)
                {
                    Destroy(spawnIndicator);
                }
                yield break;
            }
            timer += Time.deltaTime;
            yield return null;
        }

        // Détruire l'indicateur de spawn
        if (spawnIndicator != null)
        {
            Destroy(spawnIndicator);
        }

        // Vérifier si le spawning est toujours activé avant de créer l'ennemi
        if (isSpawning)
        {
            // Instancier l'ennemi avec rotation
            Instantiate(prefabToSpawn, spawnPosition, spawnRotation);
        }
    }
}
