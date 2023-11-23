using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;
    public bool isImmune = false;
    private float immuneDuration = 1f;
    private float immuneTimer = 0f;

    public float damageAmount = 1f; // Variable publique pour définir la valeur de dégâts
    public HealthBarUpdater healthBarUpdater; // Référence à HealthBarUpdater

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (isImmune)
        {
            // Update the immune timer
            immuneTimer -= Time.deltaTime;

            // Check if the immune duration is over
            if (immuneTimer <= 0f)
            {
                isImmune = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isImmune)
        {
            TakeDamage(damageAmount); // Utilisation de la variable de dégâts
            ActivateImmunity();
        }
    }

    public void TakeDamage(float damage)
    {
        if (!isImmune)
        {
            currentHealth -= Mathf.RoundToInt(damage); // Utilisation de Mathf.RoundToInt pour des dégâts entiers

            if (currentHealth <= 0)
            {
                // Player is defeated, you can add game-over logic here.
                // For now, let's reset the health.
                currentHealth = maxHealth;
            }

            // Mettez à jour la barre de vie avec la nouvelle valeur
            if (healthBarUpdater != null)
            {
                healthBarUpdater.Change(-Mathf.RoundToInt(damage));
            }
        }
    }

    void ActivateImmunity()
    {
        // Set the player immune and start the immune timer
        isImmune = true;
        immuneTimer = immuneDuration;
    }
}
