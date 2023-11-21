using System;
using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Wave Settings")]
    public int startingWave = 1;
    public int maxEnemiesPerWave = 10;
    public float breakTimeBetweenWaves = 5f;
    public float difficultyMultiplier = 1.3f;

    [Header("Enemy Spawning")]
    public Transform[] spawnPoints;
    public float spawnRate = 2f;
    public GameObject YourEnemyPrefab; // Replace "YourEnemyPrefab" with the actual prefab you want to spawn
    public event Action EnemyDestroyed; // Event for notifying enemy destruction

    private int currentWave;
    private int currentMaxEnemies;
    private int remainingEnemies;
    private bool isWaveActive;
    private float breakTimer;

    void Start()
    {
        currentWave = startingWave;
        StartNewWave();
    }

    void Update()
    {
        if (isWaveActive)
        {
            // Check if there are no enemies left
            if (remainingEnemies <= 0)
            {
                isWaveActive = false;
                breakTimer = breakTimeBetweenWaves;
            }
        }
        else
        {
            // Break time between waves
            if (breakTimer > 0)
            {
                breakTimer -= Time.deltaTime;
            }
            else
            {
                StartNewWave();
            }
        }
    }

    void StartNewWave()
    {
        currentMaxEnemies = Mathf.RoundToInt(maxEnemiesPerWave * Mathf.Pow(difficultyMultiplier, currentWave - 1));
        remainingEnemies = currentMaxEnemies;
        isWaveActive = true;
        Debug.Log("Starting wave " + currentWave + " with " + currentMaxEnemies + " enemies");

        // Spawn all enemies at the beginning of the wave
        StartCoroutine(SpawnAllEnemies());
    }

    IEnumerator SpawnAllEnemies()
{
    // Wait until the wave becomes active
    yield return new WaitUntil(() => isWaveActive);

    Debug.Log("Wave " + currentWave + " has started.");

    for (int i = 0; i < currentMaxEnemies; i++)
    {
        // Spawn an enemy
        SpawnEnemy();

        // Wait for the specified spawn rate before spawning the next enemy
        yield return new WaitForSeconds(1f / spawnRate);
    }

    Debug.Log("All enemies spawned for Wave " + currentWave + ".");

    // Wait until all enemies are destroyed before starting the break time
    yield return new WaitUntil(() => remainingEnemies == 0);

    Debug.Log("All enemies destroyed for Wave " + currentWave + ". Starting break time.");

    // Start the break time between waves
    isWaveActive = false;
    breakTimer = breakTimeBetweenWaves;

    // Wait for the break time to finish before starting the next wave
    yield return new WaitForSeconds(breakTimeBetweenWaves);

    Debug.Log("Break time finished. Starting Wave " + (currentWave + 1) + ".");

    // Increase difficulty for the next wave
    currentWave++;
}


    void SpawnEnemy()
    {
        if (!isWaveActive)
        {
            // Don't spawn new enemies if the wave is not active
            return;
        }

        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points assigned for enemies!");
            return;
        }

        // Choose a random spawn point
        Transform spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];

        // Instantiate an enemy prefab at the chosen spawn point
        // Replace "YourEnemyPrefab" with the actual prefab you want to spawn
        GameObject enemy = Instantiate(YourEnemyPrefab, spawnPoint.position, spawnPoint.rotation);

        // Subscribe to the enemy's destruction event
        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.OnEnemyDestroyed += NotifyEnemyDestroyed;
        }

        // You may want to pass additional information to the enemy, such as target or behavior settings
        // Example: enemy.GetComponent<YourEnemyScript>().SetTarget(PlayerTransform);

        // Decrement the remaining enemies count
        remainingEnemies--;
    }

    void NotifyEnemyDestroyed()
    {
        // Invoked when an enemy is destroyed
        if (EnemyDestroyed != null)
        {
            EnemyDestroyed.Invoke();
        }
    }
}
