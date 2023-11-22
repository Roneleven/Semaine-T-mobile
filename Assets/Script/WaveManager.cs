using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int numberOfWaves;
    public int totalEnemies;
    public float timeBetweenWaves;
    public GameObject enemyPrefab;
    public BonusSpawn bonusSpawn; // Reference to your BonusSpawn script
    public GameObject bonusPrefab; // Prefab to be instantiated with Gamble

    public List<Transform> spawnPoints;

    private int enemiesDestroyed = 0; // Counter for the number of enemies destroyed
    private List<Vector3> destroyedEnemyPositions = new List<Vector3>(); // List to store positions of destroyed enemies

    private void Start()
    {
        if (spawnPoints == null)
        {
            Debug.LogError("Spawn points are not assigned in the EnemySpawner!");
            return;
        }

        if (bonusSpawn == null)
        {
            Debug.LogError("BonusSpawn script reference is missing!");
            return;
        }

        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        for (int wave = 0; wave < numberOfWaves; wave++)
        {
            SpawnWave(totalEnemies);
            yield return new WaitForSeconds(timeBetweenWaves);

            float maxWaitTime = 30f; // Set a maximum wait time to avoid potential infinite loop
            float elapsedTime = 0f;

            yield return new WaitUntil(() =>
            {
                elapsedTime += Time.deltaTime;
                return GameObject.FindGameObjectsWithTag("Enemy").Length == 0 || elapsedTime >= maxWaitTime;
            });

            if (enemiesDestroyed % 10 == 0 && enemiesDestroyed > 0)
            {
                // Call Gamble and pass the position of the 10th destroyed enemy
                if (destroyedEnemyPositions.Count >= 10)
                {
                    Vector3 positionOf10thDestroyedEnemy = destroyedEnemyPositions[destroyedEnemyPositions.Count - 10];
                    bonusSpawn.Gamble(positionOf10thDestroyedEnemy);
                }
            }
        }
    }

    void SpawnWave(int numberOfEnemies)
    {
        if (spawnPoints == null || spawnPoints.Count == 0)
        {
            Debug.LogError("No spawn points available for enemy spawning!");
            return;
        }

        int enemiesPerSpawner = Mathf.CeilToInt((float)numberOfEnemies / spawnPoints.Count);

        foreach (Transform spawnPoint in spawnPoints)
        {
            for (int i = 0; i < Mathf.Min(enemiesPerSpawner, numberOfEnemies); i++)
            {
                GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
                enemy.GetComponent<EnemyScript>().SetEnemySpawner(this); // Set reference to this EnemySpawner in the enemy script
                numberOfEnemies--;
            }
        }
    }

    // Call this method when an enemy is destroyed
    public void EnemyDestroyed(Vector3 enemyPosition)
    {
        enemiesDestroyed++;
        destroyedEnemyPositions.Add(enemyPosition);
    }
}
