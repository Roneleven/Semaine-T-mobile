using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaticleDeath : MonoBehaviour
{
    public float dur� = 2;
    // Start is called before the first frame update
    void Start()
    {
        // Invoke the DestroySelf method after 2 seconds
        Invoke("DestroySelf", dur�);
    }

    // Method to destroy the GameObject
    void DestroySelf()
    {
        // Destroy the GameObject this script is attached to
        Destroy(gameObject);
    }
}
