using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;
    public bool isImmune = false;
    private float immuneDuration = 1f;
    private float immuneTimer = 0f;

    public float damageAmount = 1f; // Variable publique pour d�finir la valeur de d�g�ts
    public HealthBarUpdater healthBarUpdater; // R�f�rence � HealthBarUpdater
    public GameObject gameOverUI; // R�f�rence � votre UI Game Over

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
            TakeDamage(damageAmount); // Utilisation de la variable de d�g�ts
            FMODUnity.RuntimeManager.PlayOneShot("event:/Player/hit");
            ActivateImmunity();
        }
    }

    public void TakeDamage(float damage)
    {
        if (!isImmune)
        {
            currentHealth -= Mathf.RoundToInt(damage); // Utilisation de Mathf.RoundToInt pour des d�g�ts entiers

            if (currentHealth <= 0)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Death");
                // Player is defeated, dissociate the camera before destroying the player
                Camera.main.transform.parent = null;
                Destroy(gameObject);

                // Activer l'UI Game Over
                if (gameOverUI != null)
                {
                    gameOverUI.SetActive(true);
                    Time.timeScale = 0f;
                }
            }

            // Mettez � jour la barre de vie avec la nouvelle valeur
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
