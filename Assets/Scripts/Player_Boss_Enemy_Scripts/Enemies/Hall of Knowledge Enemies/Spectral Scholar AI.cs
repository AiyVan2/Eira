using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectralScholarAI : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public float teleportCooldown = 5.0f;
    public float attackCooldown = 3.0f;
    public GameObject projectilePrefab;
    public Transform[] teleportPositions;
    public float detectionRange = 10.0f;

    private Transform player;
    private bool isTeleporting = false;
    private bool isAttacking = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(TeleportRoutine());
        StartCoroutine(AttackRoutine());
    }

    private void Update()
    {
        if (PlayerInRange() && !isTeleporting && !isAttacking)
        {
            FacePlayer();
        }
    }

    private bool PlayerInRange()
    {
        return player != null && Vector3.Distance(transform.position, player.position) <= detectionRange;
    }

    private void FacePlayer()
    {
        if (player == null) return;

        Vector3 direction = player.position - transform.position;
        if (direction.x > 0 && transform.localScale.x < 0)
        {
            Flip();
        }
        else if (direction.x < 0 && transform.localScale.x > 0)
        {
            Flip();
        }
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private IEnumerator TeleportRoutine()
    {
        while (true)
        {
            if (!isTeleporting && PlayerInRange())
            {
                isTeleporting = true;
                Teleport();
                yield return new WaitForSeconds(teleportCooldown);
                isTeleporting = false;
            }
            yield return null;
        }
    }

    private void Teleport()
    {
        if (player == null) return;

        int randomIndex = Random.Range(0, teleportPositions.Length);
        transform.position = teleportPositions[randomIndex].position;
    }

    private IEnumerator AttackRoutine()
    {
        while (true)
        {
            if (!isAttacking && PlayerInRange())
            {
                isAttacking = true;
                Attack();
                yield return new WaitForSeconds(attackCooldown);
                isAttacking = false;
            }
            yield return null;
        }
    }

    private void Attack()
    {
        if (projectilePrefab != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            Vector3 spawnPosition = transform.position + direction * 0.5f;
            GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * moveSpeed;
            }
        }
    }
}