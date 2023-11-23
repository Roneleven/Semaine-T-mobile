using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthup : MonoBehaviour
{
    public Health healthScript;
    // Start is called before the first frame update
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Health healthScript = other.GetComponent<Health>();
            // Increase health by 2 using the Health script reference

            healthScript.currentHealth += 1;


            // Optional: Add any other logic or effects you want when health is increased , mettre ui a jour

            // Destroy the object that increased the health (assuming it's a power-up or similar)
            Destroy(gameObject);
        }
    }
}
