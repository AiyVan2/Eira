using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alapapangalan : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float teleportCooldown = 5.0f;
    public float attackCooldown = 1.0f;
    public float idleDuration = 2.0f;
    public GameObject attackPrefab; // Prefab for attack
    public Transform[] topAttackLocations; // 6 locations for top attack
    public Transform[] sideAttackLocations; // 3 locations for side attack

    public Transform topPosition; // Position where the boss teleports to the top
    public Transform leftPosition; // Position where the boss teleports to the left side
    public Transform rightPosition; // Position where the boss teleports to the right side
    public Transform idlePosition; // Position where the boss idles

    private Transform player;
    private bool isAttacking = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(BossRoutine());
    }

    private IEnumerator BossRoutine()
    {
        while (true)
        {
            // Attack when at the top
            yield return TeleportAndAttack(topPosition.position, true);

            // Attack from the left side
            yield return TeleportAndAttack(leftPosition.position, false);

            // Move to the right side and attack
            yield return TeleportAndAttack(rightPosition.position, false);

            // Idle in the middle
            yield return Idle();
        }
    }

    private IEnumerator TeleportAndAttack(Vector3 teleportPosition, bool isTop)
    {
        // Teleport to the new position
        transform.position = teleportPosition;

        if (isTop)
        {
            // Spawn attack prefabs at all 6 top locations
            SpawnAttackPrefabs(topAttackLocations);
        }
        else
        {
            // Spawn attack prefabs at 3 side locations
            SpawnAttackPrefabs(sideAttackLocations);
        }

        // Wait for attack cooldown
        yield return new WaitForSeconds(attackCooldown);
    }

    private void SpawnAttackPrefabs(Transform[] locations)
    {
        foreach (Transform location in locations)
        {
            Instantiate(attackPrefab, location.position, Quaternion.identity);
        }
    }

    private IEnumerator Idle()
    {
        // Idle in the middle
        if (idlePosition != null)
        {
            transform.position = idlePosition.position;
        }
        else
        {
            Debug.LogWarning("Idle position not set for " + gameObject.name);
        }

        yield return new WaitForSeconds(idleDuration);
    }
}