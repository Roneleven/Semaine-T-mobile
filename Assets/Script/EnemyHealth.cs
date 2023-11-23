using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    // Event for notifying enemy destruction
    //public event Action OnEnemyDestroyed;
    public event Action<Transform> OnEnemyDestroyed;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            // Invoke the event when the enemy is destroyed
            NotifyEnemyDestroyed();

            // Perform other destruction logic if needed
            Destroy(gameObject);
        }
    }

    void NotifyEnemyDestroyed()
    {
        // Invoked when the enemy is destroyed
        if (OnEnemyDestroyed != null)
        {
            //OnEnemyDestroyed.Invoke();
            OnEnemyDestroyed.Invoke(transform);
        }
    }
}
