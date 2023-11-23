using UnityEngine;

public class BulletMultiplier : MonoBehaviour
{
    // Cette fonction est appelée lorsqu'un autre collider entre en collision avec ce collider
    void OnCollisionEnter(Collision collision)
    {
        // Vérifie si l'objet entrant en collision est le joueur
        if (collision.gameObject.tag == "Player")
        {
            // Augmente bulletCount de 1
            AutoShooterWithMovement shooter = collision.gameObject.GetComponent<AutoShooterWithMovement>();
            if (shooter != null)
            {
                shooter.bulletCount++;
            }

            // Détruit cet objet
            Destroy(gameObject);
        }
    }
}