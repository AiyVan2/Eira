using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnchantedQuillAI : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float attackRange = 5.0f;
    public float projectileSpeed = 5.0f;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public GameObject inkPoolPrefab;
    public float inkPoolDuration = 3.0f;
    public float inkPoolDamage = 5.0f;

    private Transform player;
    private bool isAttacking = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(AttackRoutine());
    }

    private void Update()
    {
        if (!isAttacking)
        {
            MoveTowardsPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        if (player == null) return;

        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    private IEnumerator AttackRoutine()
    {
        while (true)
        {
            if (Vector3.Distance(transform.position, player.position) <= attackRange)
            {
                isAttacking = true;
                ShootProjectile();
                yield return new WaitForSeconds(1.0f); // Time between attacks
                CreateInkPool();
                yield return new WaitForSeconds(inkPoolDuration); // Duration of ink pool
                isAttacking = false;
            }
            else
            {
                isAttacking = false;
            }

            yield return null;
        }
    }

    private void ShootProjectile()
    {
        if (projectilePrefab != null && projectileSpawnPoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = (player.position - projectileSpawnPoint.position).normalized;
                rb.velocity = direction * projectileSpeed;
            }
        }
    }

    private void CreateInkPool()
    {
        if (inkPoolPrefab != null)
        {
            GameObject inkPool = Instantiate(inkPoolPrefab, transform.position, Quaternion.identity);
            Destroy(inkPool, inkPoolDuration);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Handle player interaction with ink pool or projectiles
            // e.g., applying damage, slowing down, etc.
        }
    }
}
