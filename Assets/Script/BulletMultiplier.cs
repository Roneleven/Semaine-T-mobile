using UnityEngine;

public class BulletMultiplier : MonoBehaviour
{
    public GameObject particle;
    // Cette fonction est appelée lorsqu'un autre collider entre en collision avec ce collider
    //void OnCollisionEnter(Collision collision)
    //{
    //    // Vérifie si l'objet entrant en collision est le joueur
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        // Augmente bulletCount de 1
    //        AutoShooterWithMovement shooter = collision.gameObject.GetComponent<AutoShooterWithMovement>();
    //        if (shooter != null)
    //        {
    //            shooter.bulletCount++;
    //        }
    //        Instantiate(particle, transform.position, Quaternion.identity);
    //        // Détruit cet objet
    //        Destroy(gameObject);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Collectibles/Collect");
            AutoShooterWithMovement shooter = other.GetComponent<AutoShooterWithMovement>();

            shooter.bulletCount++;

            // Optional: Add any other logic or effects you want when health is increased , mettre ui a jour
            Instantiate(particle, transform.position, Quaternion.identity);
            // Destroy the object that increased the health (assuming it's a power-up or similar)
            Destroy(gameObject);
        }
    }
}