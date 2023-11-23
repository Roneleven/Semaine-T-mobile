using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootUp : MonoBehaviour
{
    public AutoShooterWithMovement shoot;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AutoShooterWithMovement shoot = other.GetComponent<AutoShooterWithMovement>();
            // Increase health by 2 using the Health script reference
            shoot.fireRate += 0.2f;

            // Optional: Add any other logic or effects you want when health is increased , mettre ui a jour

            // Destroy the object that increased the health (assuming it's a power-up or similar)
            Destroy(other.gameObject);
        }
    }
}
