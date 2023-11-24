using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private static int enemiesDestroyed = 0; // Keep track of the total number of enemies destroyed

    // Event for notifying enemy destruction
    public static event Action<Transform, Vector3> OnEnemyDestroyed; // Pass both Transform and Vector3 for position

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        FMODUnity.RuntimeManager.PlayOneShot("event:/Enemys/EnemyDead", GetComponent<Transform>().position);

        if (currentHealth <= 0)
        {
            // Capture the position before destroying the enemy
            Vector3 deathPosition = transform.position;

            // Increment the total number of enemies destroyed
            enemiesDestroyed++;

            // Invoke the event when the enemy is destroyed, passing both Transform and death position
            NotifyEnemyDestroyed(deathPosition);

            // Perform other destruction logic if needed
            FMODUnity.RuntimeManager.PlayOneShot("event:/Enemys/EnemyHit", GetComponent<Transform>().position);
            Destroy(gameObject);
        }
    }

    void NotifyEnemyDestroyed(Vector3 deathPosition)
    {
        // Invoked when the enemy is destroyed
        if (OnEnemyDestroyed != null)
        {
            // Pass both the Transform and death position to the subscribers
            OnEnemyDestroyed.Invoke(transform, deathPosition);
        }
    }

    public static int GetEnemiesDestroyedCount()
    {
        return enemiesDestroyed;
    }

    public static void ResetEnemiesDestroyedCount()
    {
        enemiesDestroyed = 0;
    }
}
