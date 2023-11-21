using UnityEngine;
using System.Collections.Generic;

public class AutoShooterWithMovement : MonoBehaviour
{
    public float shootingRadius = 5f;
    public LayerMask enemyLayer;
    public Transform gunTransform;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float moveSpeed = 5f;
    public float fireRate = 0.5f; // Adjust the fire rate as needed
    private float fireCooldown = 0f;

    void Update()
    {
        // Handle player movement
        Move();

        // Detect enemies within the shooting radius
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, shootingRadius, enemyLayer);

        // Check if there are any enemies within the shooting radius and if the cooldown is over
        if (hitColliders.Length > 0 && Time.time > fireCooldown)
        {
            // Find the closest enemy
            Transform closestEnemy = FindClosestEnemy(hitColliders);

            // Aim at the closest enemy
            AimAt(closestEnemy);

            // Shoot at the closest enemy
            Shoot();

            // Reset the fire cooldown
            fireCooldown = Time.time + 1f / fireRate;
        }
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical) * moveSpeed * Time.deltaTime;
        transform.Translate(movement, Space.World);
    }

    void AimAt(Transform target)
    {
        if (target != null)
        {
            // Rotate towards the target
            gunTransform.LookAt(target);
        }
    }

    void Shoot()
    {
        // Instantiate a bullet and set its position and rotation
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Add force to the bullet to make it move
        BulletController bulletController = bullet.GetComponent<BulletController>();
        if (bulletController != null)
        {
            bulletController.SetBulletSpeed(10f);
        }
    }

    Transform FindClosestEnemy(Collider[] enemies)
    {
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (var enemyCollider in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemyCollider.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemyCollider.transform;
            }
        }

        return closestEnemy;
    }

    // Draw the shooting area in the Scene view for better visualization
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRadius);
    }
}
