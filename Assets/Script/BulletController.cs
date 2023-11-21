using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public int damage = 20; // Adjust the damage amount as needed

    void Update()
    {
        // Move the bullet forward
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);

        // Destroy the bullet after a certain time to prevent it from moving indefinitely
        Destroy(gameObject, 2f);
    }

    public void SetBulletSpeed(float speed)
    {
        bulletSpeed = speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has an EnemyHealth component
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            // Apply damage to the enemy
            enemyHealth.TakeDamage(damage);

            // Destroy the bullet after hitting the enemy
            Destroy(gameObject);
        }
    }
}
