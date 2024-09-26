using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NebulaPhantomAI : MonoBehaviour
{
    public Transform[] teleportPoints;  // Array to store the 4 teleport locations
    public GameObject homingProjectilePrefab; // Prefab for the projectile it shoots
    public Transform player;            // Reference to the player
    public float teleportCooldown = 3f; // Time between each teleport
    public float projectileSpeed = 5f;  // Speed of the projectiles
    public Transform shootPoint;        // The point from which it shoots the projectile
    public float attackDelay = 1f;      // Delay before shooting after teleporting
    public float detectionRange = 10f;  // Range at which the phantom detects the player

    private int currentTeleportIndex = -1; // To track which teleport point the phantom is at

    private void Start()
    {
        StartCoroutine(TeleportAndAttack()); // Start the teleport and attack cycle
    }

    // Coroutine to handle teleportation and attack cycle
    private IEnumerator TeleportAndAttack()
    {
        while (true)
        {
            // Check if the player is within detection range
            if (Vector2.Distance(transform.position, player.position) <= detectionRange)
            {
                // Teleport to a random point
                Teleport();

                // Wait before shooting
                yield return new WaitForSeconds(attackDelay);

                // Shoot at the player
                ShootHomingProjectile();

                // Wait for the teleport cooldown before teleporting again
                yield return new WaitForSeconds(teleportCooldown);
            }
            else
            {
                // Wait for a short period before checking again
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    // Teleports the Nebula Phantom to a random point in the teleportPoints array
    private void Teleport()
    {
        int randomIndex = currentTeleportIndex;

        // Ensure the phantom doesn't teleport to the same location consecutively
        while (randomIndex == currentTeleportIndex)
        {
            randomIndex = Random.Range(0, teleportPoints.Length);
        }

        currentTeleportIndex = randomIndex;
        transform.position = teleportPoints[randomIndex].position;
    }

    // Shoots a homing projectile toward the player
    private void ShootHomingProjectile()
    {
        if (player != null)
        {
            // Create the projectile at the shoot point
            GameObject homingProjectile = Instantiate(homingProjectilePrefab, shootPoint.position, Quaternion.identity);

            // Pass the player to the homing projectile to follow
            HomingProjectile projectileScript = homingProjectile.GetComponent<HomingProjectile>();
            if (projectileScript != null)
            {
                projectileScript.Initialize(player, projectileSpeed); // Pass the player and speed to the projectile
            }
        }
    }
}