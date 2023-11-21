using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public List<Transform> spawnPoints = new List<Transform>();
    public float spawnInterval = 3f;
    public int maxEnemiesPerWave = 5;
    public float timeBetweenWaves = 10f;

    private int currentWave = 0;
    private int enemiesRemaining;
    private float timer = 0f;

    void Start()
    {
        StartNextWave();
    }

    void Update()
    {
        if (enemiesRemaining == 0)
        {
            // No enemies remaining, give the player a break before starting the next wave
            timer += Time.deltaTime;

            if (timer >= timeBetweenWaves)
            {
                // Start the next wave
                StartNextWave();
                timer = 0f;
            }
        }
    }

    void StartNextWave()
    {
        currentWave++;
        enemiesRemaining = maxEnemiesPerWave * currentWave;

        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < maxEnemiesPerWave * currentWave; i++)
        {
            // Choose a random spawn point from the list
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

            // Instantiate a new enemy at the chosen spawn point
            GameObject newEnemy = Instantiate(enemyPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);

            // Set enemy health based on the current wave
            EnemyHealth enemyHealth = newEnemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.maxHealth = currentWave * 20; // Adjust the scaling factor as needed
                // If there's no 'StartingHealth' method, you can remove the following line
                // enemyHealth.StartingHealth(); // Reset the enemy's health
            }

            // Increment the enemies spawned and decrement the remaining enemies
            // This ensures accurate tracking of spawned and remaining enemies
            i++;
            enemiesRemaining--;

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
