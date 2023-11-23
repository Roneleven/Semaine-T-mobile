using UnityEngine;
using UnityEngine.UI;

public class HealthBarUpdater : MonoBehaviour
{
    public Health playerHealth;
    public Image healthBar;

    void Start()
    {
        if (playerHealth == null)
        {
            Debug.LogError("Player Health script not assigned!");
        }

        if (healthBar == null)
        {
            Debug.LogError("Health Bar Image not assigned!");
        }
    }

    void Update()
    {
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        if (playerHealth != null && healthBar != null)
        {
            float healthPercentage = (float)playerHealth.currentHealth / playerHealth.maxHealth;
            healthBar.fillAmount = healthPercentage;
        }
    }
}
