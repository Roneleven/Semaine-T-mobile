using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        // Check if the enemy's health has reached zero
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Add any death behavior here (e.g., play death animation, drop items, etc.)
        Destroy(gameObject);
    }
}
