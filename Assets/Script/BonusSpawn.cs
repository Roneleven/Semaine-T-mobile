using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawn : MonoBehaviour
{
    private int bad;
    private Health healthScript;
    public GameObject hP_up;
    public GameObject speedup;
    public GameObject shootup;
    public GameObject boom;
   
    // ...

    void Update()
    {
        Gamble(transform.position);
        
        if (bad >= 10)
        {
            // Modify the following line to pass the position to the Gamble method
            //Gamble(Vector3.zero); // Default value; replace with the actual position
        }
    }

    // Modify the Gamble method to accept a Vector3 parameter for the position                          
    public void Gamble(Vector3 spawnPosition)
    {
        // Generate a random number between 1 and 4 (inclusive)
        int randomNumber = Random.Range(1, 5);

        // Deliver different messages based on the random number
        switch (randomNumber)
        {
            case 1:
                Instantiate(hP_up, spawnPosition, Quaternion.identity);
                Debug.Log("Hell");
                break;
            case 2:
                Instantiate(speedup, spawnPosition, Quaternion.identity);
                Debug.Log("Hi");
                break;
            case 3:
                Instantiate(shootup, spawnPosition, Quaternion.identity);
                Debug.Log("LOL");
                break;
            case 4:
                Instantiate(boom, spawnPosition, Quaternion.identity);
                Debug.Log("Ayo");
                break;
            default:
                Debug.LogError("Unexpected random number: " + randomNumber);
                break;
        }

        bad = 0;
    }
    private void OnEnable()
    {
        // Subscribe to the OnEnemyDestroyed event in EnemyHealth
        GetComponent<EnemyHealth>().OnEnemyDestroyed += HandleEnemyDestroyed;
    }

    private void OnDisable()
    {
        // Unsubscribe to prevent memory leaks
        GetComponent<EnemyHealth>().OnEnemyDestroyed -= HandleEnemyDestroyed;
    }

    // Handle the enemy destroyed event and spawn the bonus at the enemy's position
    private void HandleEnemyDestroyed(Transform enemyTransform)
    {
        Gamble(enemyTransform.position);
    }
}
