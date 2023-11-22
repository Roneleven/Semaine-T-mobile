using UnityEngine;
using UnityEngine.AI;

public class ChargingUnit : MonoBehaviour
{
    public string playerTag = "Player"; // Set the tag of the player

    private Transform player; // Reference to the player's transform
    private NavMeshAgent navMeshAgent;

    float baseSpeed = 2.0f; // Set your desired base speed here

    [Header("Charging Settings")]
    public float chargeDistance = 5.0f; // Set the charge distance where the object will start charging faster
    public float chargeSpeed = 10.0f; // Set the charge speed when the object is close to the player
    public float pauseDurationInSeconds = 2.0f; // Set the pause duration in seconds

    private float pauseTimer;
    private bool isPaused = false;

    void Start()
    {
        // Find the player GameObject using the specified tag
        GameObject playerObject = GameObject.FindGameObjectWithTag(playerTag);

        // Get the NavMeshAgent component attached to this GameObject
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player not found. Make sure the player has the specified tag.");
        }

        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent component not found on this GameObject.");
        }
    }

    void Update()
{
    if (player != null && navMeshAgent != null)
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        Debug.Log("Distance to Player: " + distanceToPlayer);

        // Check if the ChargingUnit is not currently paused and is entering the charge distance
        if (!isPaused && distanceToPlayer <= chargeDistance)
        {
            Debug.Log("Entering charge distance. Pausing...");

            // Start the pause timer when entering the charge distance
            isPaused = true;
            pauseTimer = 0f;

            // Completely stop moving during the pause
            navMeshAgent.isStopped = true;

            Debug.Log("Pause started. Movement stopped. Timer reset.");
        }

        // If the ChargingUnit is currently paused, update the pause timer
        if (isPaused)
        {
            Debug.Log("Charging paused. Pause Timer: " + pauseTimer);

            // Update the pause timer
            pauseTimer += Time.deltaTime;

            if (pauseTimer >= pauseDurationInSeconds)
            {
                // Resume charging toward the player after the pause
                isPaused = false;

                // Ensure that the ChargingUnit is set back to charging mode
                navMeshAgent.SetDestination(player.position);
                navMeshAgent.speed = chargeSpeed; // Use charge speed during charging

                // Resume movement
                navMeshAgent.isStopped = false;

                Debug.Log("Pause duration reached. Resuming charging. Movement resumed...");
            }
        }
        else
        {
            // If the player is outside the charge distance, continue normal movement
            navMeshAgent.SetDestination(player.position);
            navMeshAgent.speed = baseSpeed; // Use base speed when not in charging mode

            Debug.Log("Player outside charge distance. Resuming normal movement...");
        }
    }
}




}
