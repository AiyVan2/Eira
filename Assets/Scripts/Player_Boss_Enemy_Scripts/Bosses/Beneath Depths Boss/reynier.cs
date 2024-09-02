using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class reynier : MonoBehaviour
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

    private bool isAttacking = false;
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= detectionRange)
        {
            FacePlayer();

            if (!isAttacking)
            {
                MoveTowardsPlayer();

                if (distanceToPlayer <= attackRange && !isAttacking)
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
    }

    private IEnumerator AttackPlayer()
    {
        isAttacking = true;

        // Randomly decide whether to perform a melee attack or a ranged attack first
        bool performRangedAttackFirst = Random.value > 0.5f;

        if (performRangedAttackFirst)
        {
            yield return StartCoroutine(ProjectileAttackPlayer());
        }

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

        if (!performRangedAttackFirst)
        {
            yield return StartCoroutine(ProjectileAttackPlayer());
        }

        isAttacking = false;

        // Add delay before the next attack sequence starts
        yield return new WaitForSeconds(attackDelay);
    }

    private IEnumerator ProjectileAttackPlayer()
    {
        animator.SetBool("isRangeAttacking", true);
        yield return new WaitForSeconds(0.45f);
        animator.SetBool("isRangeAttacking", false);
        SpawnProjectile();
    }


    private void SpawnProjectile()
    {
        // Determine the direction based on the boss's scale (whether facing right or left)
        float facingDirection = transform.localScale.x > 0 ? 1f : -1f;

        // Spawn the right projectile (which should go to the right if facing right, left if facing left)
        GameObject projectileRight = Instantiate(projectileAttack, rightSpawnPoint.position, Quaternion.identity);
        projectileRight.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed * facingDirection, 0f);

        // Spawn the left projectile (which should go to the left if facing right, right if facing left)
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
}