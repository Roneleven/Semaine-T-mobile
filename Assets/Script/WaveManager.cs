using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private List<int> enemiesPerWave;
    [SerializeField] private float timeBetweenWaves;
    [SerializeField] private float timeBetweenEnemySpawns;
    [SerializeField] private List<GameObject> enemyPrefabs;
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
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
