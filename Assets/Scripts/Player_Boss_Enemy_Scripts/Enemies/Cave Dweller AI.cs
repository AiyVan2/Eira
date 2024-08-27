using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveDwellerAI : MonoBehaviour
{
    public GameObject player;
    public GameObject attack;
    public float detectionRange = 5f; 
    public float attackRange = 2f; 
    public float moveSpeed = 2f; 

    private bool isAttacking = false;
    public Animator animator;


    private void Update()
    {
        

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= detectionRange)
        {
            FacePlayer();

            if (!isAttacking)
            {
                MoveTowardsPlayer();
                animator.SetBool("isWalking", true);
            }

            if (distanceToPlayer <= attackRange && !isAttacking)
            {
                animator.SetBool("isWalking", false);
                StartCoroutine(AttackPlayer());

            }
        }
    }

    private IEnumerator AttackPlayer()
    {
        isAttacking = true;
        attack.SetActive(true);
        animator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(0.3f);
        attack.SetActive(false);
        animator.SetBool("isAttacking", false);
        yield return new WaitForSeconds(3f);
        isAttacking = false;
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