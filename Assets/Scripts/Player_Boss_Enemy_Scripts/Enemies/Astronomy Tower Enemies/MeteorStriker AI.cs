using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorStrikerAI : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the meteor striker
    public float chargeDistance = 10f; // Distance at which it charges at the player
    public float fireTrailDuration = 2f; // Duration the fire trail lasts
    public GameObject fireTrailPrefab; // Fire trail prefab to instantiate
    public GameObject fragmentPrefab; // Prefab for the fragments to instantiate on explosion
    public int fragmentCount = 5; // Number of fragments to spawn
    public float explosionForce = 5f; // Force applied to fragments
    public float attackCooldown = 3f; // Time before it can charge again
    public Transform player; // Reference to the player
    private bool isCharging = false;
    private bool canAttack = true; // Control if the enemy can attack


    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (!isCharging && canAttack)
        {
            // Check distance to player and initiate charge if within range
            if (Vector2.Distance(transform.position, player.position) < chargeDistance)
            {
                StartCoroutine(ChargeAtPlayer());
                FacePlayer();
            }
        }
    }

    private IEnumerator ChargeAtPlayer()
    {
        isCharging = true;

        // Calculate direction towards the player
        Vector2 direction = (player.position - transform.position).normalized;

        // Dash towards the player for a fixed distance or time
        float dashTime = 1f; // adjust the dash time to your liking
        float timer = 0f;
        animator.SetBool("Dashing", true);
        yield return new WaitForSeconds(0.7f);
        GameObject fireTrail = Instantiate(fireTrailPrefab, transform.position, Quaternion.identity);
        Destroy(fireTrail, fireTrailDuration);


        while (timer < dashTime)
        {
            
            transform.position = new Vector2(transform.position.x, transform.position.y) + direction * moveSpeed * Time.deltaTime;
            timer += Time.deltaTime;
            yield return null;
            
        }
        animator.SetBool("Dashing", false);

        animator.SetBool("Attacking", true);
        yield return new WaitForSeconds(0.3f);
        Explode();
        animator.SetBool("Attacking", false);
        canAttack = false; // Prevent further attacks until cooldown is over
        yield return new WaitForSeconds(attackCooldown); // Wait for cooldown
        canAttack = true; // Reset the ability to attack
        isCharging = false; // Reset charging state
    }

    private void Explode()
    {
        for (int i = 0; i < fragmentCount; i++)
        {
            // Instantiate a fragment
            GameObject fragment = Instantiate(fragmentPrefab, transform.position, Quaternion.identity);

            // Calculate direction for the fragment
            Vector2 explosionDirection = Random.insideUnitCircle.normalized; // Random direction
            Rigidbody2D rb = fragment.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Apply force to the fragment
                rb.AddForce(explosionDirection * explosionForce, ForceMode2D.Impulse);
            }
        }
    }

    private void FacePlayer()

    {
        // Calculate direction towards the player
        Vector2 direction = (player.position - transform.position).normalized;

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
}
