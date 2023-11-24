using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawn : MonoBehaviour
{
    public GameObject deathParticle;
    public GameObject hP_up;
    public GameObject speedup;
    public GameObject shootup;
    public GameObject boom;

    private Vector3 lastEnemyDeathPosition; // Store the position of the last killed enemy

    // ...

    void Update()
    {
        int randomCount = UnityEngine.Random.Range(10, 20);

    if (EnemyHealth.GetEnemiesDestroyedCount() >= randomCount)
        {
            // Reset the count to prevent continuous spawning
            EnemyHealth.ResetEnemiesDestroyedCount();

            // Use the position of the last killed enemy as the spawn position
            Gamble(lastEnemyDeathPosition);
        }
    }

    public void Splat(Vector3 spawnPosition)
    {
        Instantiate(deathParticle, spawnPosition, Quaternion.identity);
    }

    // Modify the Gamble method to accept a Vector3 parameter for the position
    public void Gamble(Vector3 spawnPosition)
    {
        // Generate a random number between 1 and 4 (inclusive)
        int randomNumber = Random.Range(1, 5);

        // Deliver different messages based on the random number
        switch (randomNumber)
        {
            case 1:
                Instantiate(hP_up, spawnPosition, Quaternion.identity);
                break;
            case 2:
                Instantiate(speedup, spawnPosition, Quaternion.identity);
                break;
            case 3:
                Instantiate(shootup, spawnPosition, Quaternion.identity);
                break;
            case 4:
                Instantiate(boom, spawnPosition, Quaternion.identity);
                break;
            default:
                Debug.LogError("Unexpected random number: " + randomNumber);
                break;
        }

        // Reset the count to prevent continuous spawning
        EnemyHealth.ResetEnemiesDestroyedCount();
    }

    private void OnEnable()
    {
        // Subscribe to the OnEnemyDestroyed event in EnemyHealth
        EnemyHealth.OnEnemyDestroyed += HandleEnemyDestroyed;
    }

    private void OnDisable()
    {
        // Unsubscribe to prevent memory leaks
        EnemyHealth.OnEnemyDestroyed -= HandleEnemyDestroyed;
    }

    // Handle the enemy destroyed event and store the position of the last killed enemy
    private void HandleEnemyDestroyed(Transform enemyTransform, Vector3 deathPosition)
    {
        lastEnemyDeathPosition = deathPosition;
        Splat(lastEnemyDeathPosition);
    }
}


