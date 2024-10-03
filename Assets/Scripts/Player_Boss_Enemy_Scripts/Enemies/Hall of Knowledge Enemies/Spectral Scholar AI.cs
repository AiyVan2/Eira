using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectralScholarAI : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public float teleportCooldown = 5.0f;
    public float attackCooldown = 3.0f;
    public GameObject homingProjectilePrefab;
    public Transform[] teleportPositions;
    public float detectionRange = 10.0f;
    public float projectileSpeed = 3f;
    public Transform firePoint;

    private Transform player;
    private bool isTeleporting = false;
    private bool isAttacking = false;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        StartCoroutine(TeleportRoutine());
        StartCoroutine(AttackRoutine());
    }

    private void Update()
    {
        FacePlayer();
    }

    private bool PlayerInRange()
    {
        return player != null && Vector3.Distance(transform.position, player.position) <= detectionRange;
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
    private IEnumerator TeleportRoutine()
    {
        while (true)
        {
            if (!isTeleporting && PlayerInRange())
            {
                isTeleporting = true;
                animator.SetBool("Teleporting", true);
                yield return new WaitForSeconds(0.3f);
                Teleport();
                animator.SetBool("Teleporting", false);
                animator.SetBool("Reappear", true);
                yield return new WaitForSeconds(0.3f);
                animator.SetBool("Reappear", false);
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
        if (player != null)
        {
            int numberofProjectile = 5;
            int spreadAngle = 500;
            float angleStep = spreadAngle / (numberofProjectile - 1);

            for (int i = 0; i < numberofProjectile; i++)
            {
                // Calculate the angle for this specific projectile
                float angle = -spreadAngle / 2 + angleStep * i; 

                GameObject homingProjectile = Instantiate(homingProjectilePrefab, firePoint.position, Quaternion.identity);

                homingProjectile.transform.Rotate(0,0,angle);
                // Pass the player to the homing projectile to follow
                HomingProjectile projectileScript = homingProjectile.GetComponent<HomingProjectile>();
                if (projectileScript != null)
                {
                    projectileScript.Initialize(player, projectileSpeed); // Pass the player and speed to the projectile
                }
            }
        }
    }
}