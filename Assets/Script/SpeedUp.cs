using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    public AutoShooterWithMovement speed;
    public GameObject particle;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Collectibles/Collect");
            AutoShooterWithMovement speed = other.GetComponent<AutoShooterWithMovement>();
            // Increase health by 2 using the Health script reference
            speed.moveSpeed += 1;

            // Optional: Add any other logic or effects you want when health is increased , mettre ui a jour
            Instantiate(particle, transform.position, Quaternion.identity);
            // Destroy the object that increased the health (assuming it's a power-up or similar)
            Destroy(gameObject);
        }
    }
}
