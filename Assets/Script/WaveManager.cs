using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private int numberOfWaves;
    [SerializeField] private int totalEnemies;
    [SerializeField] private float timeBetweenWaves;
    [SerializeField] private float timeBetweenEnemySpawns;
    [SerializeField] private List<GameObject> enemyPrefabs;  // Use a list of enemy prefabs instead of a single GameObject
    [SerializeField] private List<Transform> spawnPoints;

    private int currentWave = 0;

    private void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (currentWave < numberOfWaves)
        {
            int numberOfEnemies = totalEnemies;

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
        // Randomly choose a spawn point
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

        // Randomly choose an enemy prefab
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];

        // Instantiate an enemy at the chosen spawn point
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
