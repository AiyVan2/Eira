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
    public float shootingRange = 8f;        // Range at which the sentinel will start shooting

    private Vector3 startPosition;          // Starting position to calculate hover range
    private bool movingUp = true;           // To determine hover direction (up or down)
    private bool isShooting = false;        // Track if the sentinel is currently shooting
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // Audio
    public AudioManager audioManager;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPosition = transform.position; // Store the initial position to handle hovering
    }

    private void Update()
    {
        Hover();  // Handle hovering movement

        // Check if the player is within shooting range
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= shootingRange)
        {
            // Start shooting if not already shooting
            if (!isShooting)
            {
                isShooting = true;
                StartCoroutine(ShootAtPlayer());
            }
        }
        else
        {
            // Stop shooting if player is out of range
            if (isShooting)
            {
                isShooting = false;
                StopCoroutine(ShootAtPlayer());
            }
        }
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
        while (isShooting)
        {
            yield return new WaitForSeconds(shootingInterval); // Wait for the defined interval

            animator.SetBool("Attacking", true);
            yield return new WaitForSeconds(0.3f);
            audioManager.PlayAstroSentinelAttackSound();
            ShootProjectile();
            animator.SetBool("Attacking", false);
        }
    }

    // Function to shoot a projectile
    private void ShootProjectile()
    {
        if (player != null)
        {
            // Calculate direction to the player
            Vector2 direction = (player.position - shootPoint.position).normalized;

            // Create the projectile
            GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

            // Get Rigidbody2D of the projectile to apply force
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * projectileSpeed; // Set the projectile's velocity
            }

            FacePlayer();
        }
    }

    private void FacePlayer()
    {
        if (player == null) return;

        Vector3 direction = player.position - transform.position;
        // Flip the sprite based on the player's position
        if (direction.x > 0)
        {
            spriteRenderer.flipX = false; // Face right
        }
        else if (direction.x < 0)
        {
            spriteRenderer.flipX = true; // Face left
        }
    }
}