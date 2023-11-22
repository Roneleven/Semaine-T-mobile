using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int numberOfWaves;
    public int totalEnemies;
    public float timeBetweenWaves;
    public List<GameObject> enemyPrefabs; // List of enemy prefabs to choose from
    public List<Transform> spawnPoints;

    private void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        for (int wave = 0; wave < numberOfWaves; wave++)
        {
            SpawnWave(totalEnemies);
            yield return new WaitForSeconds(timeBetweenWaves);
            yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0);
        }
    }

    void SpawnWave(int numberOfEnemies)
    {
        int enemiesPerSpawner = Mathf.CeilToInt((float)numberOfEnemies / spawnPoints.Count);

        foreach (Transform spawnPoint in spawnPoints)
        {
            for (int i = 0; i < Mathf.Min(enemiesPerSpawner, numberOfEnemies); i++)
            {
                // Choose a random enemy prefab from the list
                GameObject selectedEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
                Instantiate(selectedEnemyPrefab, spawnPoint.position, spawnPoint.rotation);
                numberOfEnemies--;
            }
        }
    }
}
