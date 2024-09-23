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
    public float detectionRange = 10.0f;
    public float stopFollowRange = 3.0f;
    private float projectileLifetime = 2f;

    private Transform player;
    private bool isAttacking = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(AttackRoutine());
    }

    private void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // Only move towards player if within detection range and not within stop follow range
            if (distanceToPlayer <= detectionRange && distanceToPlayer > stopFollowRange)
            {
                MoveTowardsPlayer();
            }
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
                yield return new WaitForSeconds(3.0f); // Time between attacks
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
                Destroy(projectile, projectileLifetime);
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
}
