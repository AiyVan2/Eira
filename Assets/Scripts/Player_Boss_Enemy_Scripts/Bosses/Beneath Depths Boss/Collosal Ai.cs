using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Lurker : MonoBehaviour
{
    public GameObject player;
    public GameObject meleeAttack;
    public GameObject projectileAttack;
    public Transform rightSpawnPoint;
    public Transform leftSpawnPoint;
    public float detectionRange = 5f;
    public float attackRange = 2f;
    public float moveSpeed = 2f;
    public float projectileSpeed = 5f;
    public float attackDelay = 2f;
    public float idleTime = 2f;
    public int maxHealth = 300;

    private bool isAttacking = false;
    private Animator animator;
    private float nextActionTime = 0f;
    private int currentPhase = 1;
    private BossHealth bosshealth;
    private void Start()
    {
        animator = GetComponent<Animator>();
        bosshealth = GetComponent<BossHealth>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= detectionRange && bosshealth.health > 0)
        {
            FacePlayer();

            if (!isAttacking && Time.time >= nextActionTime)
            {
                MoveTowardsPlayer();

                if (distanceToPlayer <= attackRange)
                {
                    StartCoroutine(AttackPlayer());
                }
            }
        }
        else
        {
            animator.SetBool("isChasing", false);
            animator.SetBool("isAttacking", false);
            animator.SetBool("isRangeAttacking", false);
        }

        // Handle phase transitions based on health
        HandlePhaseTransitions();

        if (bosshealth.health <= 0)
            {
                StartCoroutine(lurkerdeathAnimation());   
            }
    }

    private void HandlePhaseTransitions()
    {
        // Phase 1: 300 - 200 HP (Only Melee Attacks)
        if (bosshealth.health > 200)
        {
            currentPhase = 1;
        }
        // Phase 2: 200 - 100 HP (Melee + Occasional Projectile Attack)
        else if (bosshealth.health <= 200 && bosshealth.health > 100)
        {
            currentPhase = 2;
        }
        // Phase 3: 100 HP or less (Increased Aggression, Melee + Frequent Projectiles)
        else
        {
            currentPhase = 3;
        }
    }

    private IEnumerator AttackPlayer()
    {
        isAttacking = true;

        switch (currentPhase)
        {
            case 1:
                // Phase 1: Only Melee Attacks
                yield return MeleeAttackSequence();
                break;

            case 2:
                // Phase 2: Melee + Occasional Projectile Attack
                if (Random.value > 0.5f)
                {
                    yield return StartCoroutine(ProjectileAttackPlayer());
                }
                yield return MeleeAttackSequence();
                break;

            case 3:
                // Phase 3: Barrage of Projectiles, Melee Attack, Barrage Again
                yield return StartCoroutine(ProjectileBarrage());
                yield return MeleeAttackSequence();
                yield return StartCoroutine(ProjectileBarrage());
                break;
        }

        // Add a delay before the next attack
        isAttacking = false;
        nextActionTime = Time.time + idleTime;

        // Add delay before the next attack sequence starts
        yield return new WaitForSeconds(attackDelay);
    }

    private IEnumerator MeleeAttackSequence()
    {
        for (int i = 0; i < 2; i++)
        {
            animator.SetBool("isAttacking", true);
            yield return new WaitForSeconds(0.6f);
            meleeAttack.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            meleeAttack.SetActive(false);
            animator.SetBool("isAttacking", false);
            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator ProjectileAttackPlayer()
    {
        animator.SetBool("isRangeAttacking", true);
        yield return new WaitForSeconds(0.45f);
        animator.SetBool("isRangeAttacking", false);
        SpawnProjectile();
    }
    private IEnumerator ProjectileBarrage()
    {
        animator.SetBool("isRangeAttacking", true);
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < 3; i++)  // Adjust the number of projectiles in the barrage as needed
        {
            SpawnProjectile();
            yield return new WaitForSeconds(0.2f);  // Adjust the delay between each projectile in the barrage
        }
        animator.SetBool("isRangeAttacking", false);
    }

    private void SpawnProjectile()
    {
        float facingDirection = transform.localScale.x > 0 ? 1f : -1f;

        GameObject projectileRight = Instantiate(projectileAttack, rightSpawnPoint.position, Quaternion.identity);
        projectileRight.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed * facingDirection, 0f);

        GameObject projectileLeft = Instantiate(projectileAttack, leftSpawnPoint.position, Quaternion.identity);
        projectileLeft.GetComponent<Rigidbody2D>().velocity = new Vector2(-projectileSpeed * facingDirection, 0f);
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        animator.SetBool("isChasing", true);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isRangeAttacking", false);
    }

    private void FacePlayer()
    {
        Vector3 scale = transform.localScale;
        if (player.transform.position.x < transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * -1;
        }
        else
        {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;
    }
   IEnumerator lurkerdeathAnimation()
    {
        animator.SetBool("isDead", true);
        yield return new WaitForSeconds(1.9f);
        Destroy(gameObject);
    }
}