using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptedSentinel : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float detectionRange;
    public float fireRate;
    private float nextFireTime;
    public Transform player;
    public Transform[] patrolPoints; // Array of patrol points
    private int currentPointIndex = 0; // Current patrol point index
    public float speed = 2.0f; // Speed of the sentinel

    private bool isFacingRight = true;
    private bool isPatrolling = true;


    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    void Start()
    {
        nextFireTime = Time.time;
        spriteRenderer = GetComponent<SpriteRenderer>(); // Cache the SpriteRenderer component
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        DetectPlayer();
        if (isPatrolling)
        {
            Patrol();
        }
    }

    void Patrol()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, step);

        // Check if we've reached the current patrol point
        if (Vector2.Distance(transform.position, patrolPoints[currentPointIndex].position) < 0.1f)
        {
            // Flip the sprite when changing direction
            if (patrolPoints[currentPointIndex].position.x < transform.position.x)
            {
                isFacingRight = false;
            }
            else
            {
                isFacingRight = true;
            }
            spriteRenderer.flipX = isFacingRight;

            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        }
    }

    void DetectPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            isPatrolling = false; 
            rb.velocity = Vector2.zero; 
            rb.constraints = RigidbodyConstraints2D.FreezePosition; 

            if (Time.time >= nextFireTime)
            {
                ShootProjectile();
                nextFireTime = Time.time + fireRate;
            }
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.None; 
            isPatrolling = true; 
        }
    }


    void ShootProjectile()
    {
        Vector2 direction = (player.position - firePoint.position).normalized;

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        projectile.transform.right = direction;
    }
}