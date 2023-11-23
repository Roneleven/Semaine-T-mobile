using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaticleDeath : MonoBehaviour
{
    public float duré = 2;
    // Start is called before the first frame update
    void Start()
    {
        // Invoke the DestroySelf method after 2 seconds
        Invoke("DestroySelf", duré);
    }

    // Method to destroy the GameObject
    void DestroySelf()
    {
        // Destroy the GameObject this script is attached to
        Destroy(gameObject);
    }
}
