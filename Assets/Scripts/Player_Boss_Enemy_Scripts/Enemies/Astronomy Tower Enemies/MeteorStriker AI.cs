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

    private void Update()
    {
        if (!isCharging && canAttack)
        {
            // Check distance to player and initiate charge if within range
            if (Vector2.Distance(transform.position, player.position) < chargeDistance)
            {
                StartCoroutine(ChargeAtPlayer());
            }
        }
    }

    private IEnumerator ChargeAtPlayer()
    {
        isCharging = true;

        // Create fire trail
        GameObject fireTrail = Instantiate(fireTrailPrefab, transform.position, Quaternion.identity);
        Destroy(fireTrail, fireTrailDuration);

        // Move towards the player
        Vector2 targetPosition = player.position;
        while (Vector2.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        Explode();
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
}
