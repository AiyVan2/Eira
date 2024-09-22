using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroSentinelAI : MonoBehaviour
{
    public float hoverSpeed = 2f;           // Speed of hovering movement
    public float hoverRange = 2f;           // Range of the up and down hovering motion
    public GameObject projectilePrefab;     // Prefab for the projectiles it will shoot
    public Transform player;                // Reference to the player
    public float shootingInterval = 2f;     // Time interval between each shot
    public float projectileSpeed = 5f;      // Speed of the projectiles
    public Transform shootPoint;            // The point from where projectiles are shot

    private Vector3 startPosition;          // Starting position to calculate hover range
    private bool movingUp = true;           // To determine hover direction (up or down)

    private void Start()
    {
        startPosition = transform.position; // Store the initial position to handle hovering
        StartCoroutine(ShootAtPlayer());    // Start shooting projectiles at intervals
    }

    private void Update()
    {
        Hover();  // Handle hovering movement
    }

    // Hovering movement
    private void Hover()
    {
        // Calculate the current hover range
        float hoverOffset = Mathf.Sin(Time.time * hoverSpeed) * hoverRange;
        transform.position = new Vector3(transform.position.x, startPosition.y + hoverOffset, transform.position.z);
    }

    // Coroutine to shoot projectiles at the player at regular intervals
    private IEnumerator ShootAtPlayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootingInterval); // Wait for the defined interval

            // Shoot a projectile toward the player
            ShootProjectile();
        }
    }

    // Function to shoot a projectile
    private void ShootProjectile()
    {
        if (player != null)
        {
            // Create the projectile
            GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

            // Calculate direction to the player
            Vector2 direction = (player.position - shootPoint.position).normalized;

            // Get Rigidbody2D of the projectile to apply force
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * projectileSpeed; // Set the projectile's velocity
            }
        }
    }
}