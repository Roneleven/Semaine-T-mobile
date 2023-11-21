using UnityEngine;
using UnityEngine.AI;

public class MoveTowardsPlayer : MonoBehaviour
{
    private Transform player; // Reference to the player's transform
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        // Find the player GameObject using a "Player" tag (adjust as needed)
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Get the NavMeshAgent component attached to this GameObject
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (player == null)
        {
            Debug.LogError("Player not found. Make sure the player has the appropriate tag.");
        }

        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent component not found on this GameObject.");
        }
        else
        {
            // Start moving towards the player
            SetDestination();
        }
    }

    void Update()
    {
        // Update the destination continuously (optional)
        SetDestination();
    }

    void SetDestination()
    {
        if (player != null && navMeshAgent != null)
        {
            // Set the destination to the player's position
            navMeshAgent.SetDestination(player.position);
        }
    }
}
