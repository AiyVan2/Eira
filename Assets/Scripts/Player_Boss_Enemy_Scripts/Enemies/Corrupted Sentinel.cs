using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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

    private bool isPatrolling = true;


    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Animator animator;


    public AudioManager audioManager;

    void Start()
    {
        nextFireTime = Time.time;
        spriteRenderer = GetComponent<SpriteRenderer>(); // Cache the SpriteRenderer component
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
        Vector3 scale = transform.localScale;
        // Check if we've reached the current patrol point
        if (Vector2.Distance(transform.position, patrolPoints[currentPointIndex].position) < 0.1f)
        {
            // Flip the sprite when changing direction
            if (patrolPoints[currentPointIndex].position.x < transform.position.x)
            {
                scale.x = Mathf.Abs(scale.x) * -1;
            }
            else
            {
                scale.x = Mathf.Abs(scale.x);
            }
            transform.localScale = scale;
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        }
    }

    void DetectPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        Vector3 scale = transform.localScale;
        if (distanceToPlayer <= detectionRange)
        {
            isPatrolling = false;
            rb.velocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezePosition;

            // Flip sprite to face the player
            if (player.position.x > transform.position.x)
            {
                scale.x = Mathf.Abs(scale.x) * -1;
            }
            else if (player.position.x < transform.position.x)
            {
                scale.x = Mathf.Abs(scale.x);
            }
            transform.localScale = scale;
            if (Time.time >= nextFireTime)
            {
                StartCoroutine(shootprojectileAnimation());
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
        Vector2 direction = new Vector2(player.position.x - firePoint.position.x,0).normalized;

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        projectile.transform.right = direction;
    }
    IEnumerator shootprojectileAnimation()
    {
        animator.SetBool("isAttacking", true);
        audioManager.PlayCorruptedSentinelAttackSound();
        yield return new WaitForSeconds(0.12f);
        ShootProjectile();
        animator.SetBool("isAttacking", false);
    }
}