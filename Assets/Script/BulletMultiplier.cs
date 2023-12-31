using UnityEngine;

public class BulletMultiplier : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Collectibles/Collect");
            AutoShooterWithMovement shooter = other.GetComponent<AutoShooterWithMovement>();

            shooter.bulletCount++;

            // Destroy the object that increased the health (assuming it's a power-up or similar)
            Destroy(gameObject);
        }
    }
}