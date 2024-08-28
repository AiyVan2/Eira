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

    private bool isAttacking = false;

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
    }

    private IEnumerator AttackPlayer()
    {
        isAttacking = true;
        for (int i = 0; i < 2; i++)
        {
            meleeAttack.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            meleeAttack.SetActive(false);
            yield return new WaitForSeconds(1f);
        }
        StartCoroutine(ProjectileAttackPlayer());
        isAttacking = false;
    }

    private IEnumerator ProjectileAttackPlayer()
    {
        yield return new WaitForSeconds(2f);
        SpawnProjectile();
        yield return new WaitForSeconds(3f);
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