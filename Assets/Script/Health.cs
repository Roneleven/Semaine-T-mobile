using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 10;
    public bool iframe = false;
    private float cooldownTime = 1f; // Set the cooldown time to 1 second
    private float cooldownTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // Setup the UI
    }

    // Update is called once per frame
    void Update()
    {
        // Setup the UI
        if (iframe)
        {
            // Update the cooldown timer
            cooldownTimer -= Time.deltaTime;

            // Check if the cooldown is over
            if (cooldownTimer <= 0f)
            {
                iframe = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CompareTag("Enemy") || !iframe)
        {
            health -= 1;
            iframe = true;
            Cooldown();
        }
    }

    public void Cooldown()
    {
        // Set the cooldown timer to 1 second
        cooldownTimer = cooldownTime;
    }
}
