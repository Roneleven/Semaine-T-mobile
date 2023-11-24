using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;
    public bool isImmune = false;
    private float immuneDuration = 1f;
    private float immuneTimer = 0f;

    public float damageAmount = 1f; // Variable publique pour définir la valeur de dégâts
    public HealthBarUpdater healthBarUpdater; // Référence à HealthBarUpdater
    public GameObject gameOverUI; // Référence à votre UI Game Over
    public Renderer playerRenderer; // Référence au Renderer du joueur (assurez-vous d'ajouter un Renderer au joueur)

    private Color originalColor;
    private Color immuneColor = new Color(1f, 0f, 0f, 0.5f); // Rouge transparent

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        // Assurez-vous qu'il y a un Renderer attaché au joueur
        if (playerRenderer != null)
        {
            originalColor = playerRenderer.material.color;
        }
        else
        {
            Debug.LogError("Veuillez attacher un Renderer au joueur.");
        }
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
                // Rétablir la couleur originale lorsque l'immunité est terminée
                playerRenderer.material.color = originalColor;
            }
            else
            {
                // Faire clignoter en rouge
                StartCoroutine(Blink());
            }
        }
    }

    IEnumerator Blink()
    {
        playerRenderer.material.color = immuneColor;
        yield return new WaitForSeconds(0.1f); // Temps pendant lequel la couleur rouge est affichée
        playerRenderer.material.color = originalColor;
        yield return new WaitForSeconds(0.1f); // Temps pendant lequel la couleur originale est affichée
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
                // Player is defeated, dissociate the camera before destroying the player
                Camera.main.transform.parent = null;
                Destroy(gameObject);

                // Activer l'UI Game Over
                if (gameOverUI != null)
                {
                    gameOverUI.SetActive(true);

                    // Mettre en pause le jeu
                    Time.timeScale = 0f;
                }
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
