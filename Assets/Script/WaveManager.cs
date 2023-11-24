using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public class EnemySpawnInfo
    {
        public GameObject enemyPrefab;
        public float spawnFrequency;
    }

    [SerializeField] private List<int> enemiesPerWave;
    [SerializeField] private float timeBetweenWaves;
    [SerializeField] private float timeBetweenEnemySpawns;
    [SerializeField] private List<EnemySpawnInfo> enemySpawnInfoList;
    [SerializeField] private List<Transform> spawnPoints;

    private int currentWave = 0;

    private void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (currentWave < enemiesPerWave.Count)
        {
            int numberOfEnemies = enemiesPerWave[currentWave];

            for (int i = 0; i < numberOfEnemies; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(timeBetweenEnemySpawns);
            }

            yield return new WaitForSeconds(timeBetweenWaves);
            yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0);

            // Move to the next wave
            currentWave++;
        }

        // All waves completed, game-over logic or other actions here.
    }

    void SpawnEnemy()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

        // Calculate total frequency
        float totalFrequency = 0f;
        foreach (var enemySpawnInfo in enemySpawnInfoList)
        {
            totalFrequency += enemySpawnInfo.spawnFrequency;
        }

        // Randomly choose an enemy type based on frequency
        float randomValue = Random.Range(0f, totalFrequency);
        float cumulativeFrequency = 0f;
        GameObject chosenEnemyPrefab = null;

        foreach (var enemySpawnInfo in enemySpawnInfoList)
        {
            cumulativeFrequency += enemySpawnInfo.spawnFrequency;

            if (randomValue <= cumulativeFrequency)
            {
                chosenEnemyPrefab = enemySpawnInfo.enemyPrefab;
                break;
            }
        }

        if (chosenEnemyPrefab != null)
        {
            Instantiate(chosenEnemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogError("No enemy prefab chosen. Check your spawn frequencies.");
        }
    }
}
