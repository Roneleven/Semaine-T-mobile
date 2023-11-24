using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Collections;

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
    public InputActionReference moveActionToUse;
    public int bulletCount = 1;
    public Rigidbody rb;
    

    void Start()
    {
        rb.GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.drag = 5f;
    }
    void Update()
{

        if (transform.position.y < 1)
        {
            transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        }
        // Handle player movement
        Move();

    // Detect enemies within the shooting radius
    Collider[] hitColliders = Physics.OverlapSphere(transform.position, shootingRadius, enemyLayer);

    // Check if there are any enemies within the shooting radius and if the cooldown is over
    if (hitColliders.Length > 0 && Time.time > fireCooldown && !IsInvoking("ShootCoroutine"))
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
        Vector2 moveDirection = moveActionToUse.action.ReadValue<Vector2>().normalized;
        Vector3 movement = new Vector3(moveDirection.x, 0f, moveDirection.y);
        rb.AddForce(movement * moveSpeed);
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
        // Commence la coroutine de tir
        StartCoroutine(ShootCoroutine());
    }

    IEnumerator ShootCoroutine()
    {
        // Utilisez une boucle for pour tirer bulletCount fois
        for (int i = 0; i < bulletCount; i++)
        {
            // Ajustez la position de départ de la balle
            Vector3 bulletStartPosition = firePoint.position + firePoint.forward * 2f;

            // Instantiate a bullet and set its position and rotation
            GameObject bullet = Instantiate(bulletPrefab, bulletStartPosition, firePoint.rotation);
            FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Tir");


            // Add force to the bullet to make it move
            BulletController bulletController = bullet.GetComponent<BulletController>();
            if (bulletController != null)
            {
                bulletController.SetBulletSpeed(10f);
            }

            // Attendez 0.1 seconde avant de tirer la prochaine balle
            yield return new WaitForSeconds(0.1f);
        }

        // Attendez fireRate secondes après avoir tiré toutes les balles
        yield return new WaitForSeconds(fireRate);
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
