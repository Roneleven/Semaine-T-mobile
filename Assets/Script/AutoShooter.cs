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
    public float fireRate = 0.5f;
    private float fireCooldown = 0f;
    public InputActionReference moveActionToUse;
    public int bulletCount = 1;
    public Rigidbody rb;
    public Animator animator;
    

    void Start()
    {
        animator = this.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Animator>();
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
        Move();

    Collider[] hitColliders = Physics.OverlapSphere(transform.position, shootingRadius, enemyLayer);

    if (hitColliders.Length > 0 && Time.time > fireCooldown && !IsInvoking("ShootCoroutine"))
    {
        Transform closestEnemy = FindClosestEnemy(hitColliders);
        AimAt(closestEnemy);

        Shoot();

        fireCooldown = Time.time + 1f / fireRate;
    }
    else
    {
      
        
    }
}
void Move()
{
    Vector2 moveDirection = moveActionToUse.action.ReadValue<Vector2>().normalized;
    Vector3 movement = new Vector3(moveDirection.x, 0f, moveDirection.y);
    rb.AddForce(movement * moveSpeed * Time.deltaTime);

    if (movement != Vector3.zero)
    {
        // Obtenez une référence à l'objet enfant
        Transform childTransform = transform.GetChild(0);

        Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
        childTransform.rotation = Quaternion.Lerp(childTransform.rotation, toRotation, moveSpeed * Time.deltaTime);
    }
}

    void AimAt(Transform target)
    {
        if (target != null)
        {
            gunTransform.LookAt(target);
        }
    }

    void Shoot()
    {
        StartCoroutine(ShootCoroutine());
    }

    IEnumerator ShootCoroutine()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            Vector3 bulletStartPosition = firePoint.position + firePoint.forward * 2f;

            GameObject bullet = Instantiate(bulletPrefab, bulletStartPosition, firePoint.rotation);
            FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Tir");


            BulletController bulletController = bullet.GetComponent<BulletController>();
            if (bulletController != null)
            {
                bulletController.SetBulletSpeed(30f);
            }

            yield return new WaitForSeconds(0.1f);
        }

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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRadius);
    }
}
