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
    public Renderer playerRenderer; // R�f�rence au Renderer du joueur (assurez-vous d'ajouter un Renderer au joueur)

    private Color originalColor;
    private Color immuneColor = new Color(1f, 0f, 0f, 0.5f); // Rouge transparent

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        // Assurez-vous qu'il y a un Renderer attach� au joueur
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
                // R�tablir la couleur originale lorsque l'immunit� est termin�e
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
        yield return new WaitForSeconds(0.1f); // Temps pendant lequel la couleur rouge est affich�e
        playerRenderer.material.color = originalColor;
        yield return new WaitForSeconds(0.1f); // Temps pendant lequel la couleur originale est affich�e
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

                    // Mettre en pause le jeu
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
