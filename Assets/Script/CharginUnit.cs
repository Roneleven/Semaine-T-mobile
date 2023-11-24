using UnityEngine;
using UnityEngine.AI;

public class ChargingUnit : MonoBehaviour
{
    public string playerTag = "Player";
    public float chargeDistance = 5.0f;
    public float chargeSpeed = 10.0f;
    public float baseSpeed = 2.0f;
    public float pauseDurationInSeconds = 2.0f;

    private Transform player;
    private NavMeshAgent navMeshAgent;
    private bool isCharging = false;
    private float pauseTimer = 0f;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag(playerTag);
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

            if (distanceToPlayer <= chargeDistance)
            {
                if (!isCharging)
                {
                    // Start charging
                    isCharging = true;
                    navMeshAgent.speed = chargeSpeed;
                }

                // Update pause timer
                pauseTimer += Time.deltaTime;

                if (pauseTimer >= pauseDurationInSeconds)
                {
                    // Reset timer and continue charging
                    pauseTimer = 0f;
                    navMeshAgent.SetDestination(player.position);
                }
            }
            else
            {
                if (isCharging)
                {
                    // Stop charging and reset timer
                    isCharging = false;
                    navMeshAgent.speed = baseSpeed;
                    pauseTimer = 0f;
                }

                // Move towards the player at base speed
                navMeshAgent.SetDestination(player.position);
            }
        }
    }
}
