using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestGuardianMovements : MonoBehaviour
{
    public Transform player;                      // Reference to the player's transform
    public Transform bossPhaseTwoAttackLocation;  // Reference to the attack location
    public GameObject[] airAttacks;               // Array of air attack GameObjects
    public string airAttackPosTag = "AirAttackPos";  // Tag of the air attack positions
    public float walkSpeed = 2f;                  // Speed at which the boss walks
    public float actionInterval = 2f;             // Time to wait before the next action
    public float teleportChance = 0.5f;           // Probability of teleporting to attack location
    public float airAttackChance = 0.25f;         // Probability of performing air attack
    public LayerMask ground;                      // LayerMask to specify the ground layer
    public int damage;

    private Rigidbody2D rb;
    private Collider2D col;
    private bool isGrounded;
    private GameObject[] airAttackPos;  // Array to store air attack position GameObjects
    private Animator animator;          // Animator component
    private bool facingRight = true;    // To track the direction the boss is facing
    private bool isPerformingAction = false;  // To track if the boss is performing an action

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>(); // Get the Animator component

        // Find air attack position GameObjects by tag
        airAttackPos = GameObject.FindGameObjectsWithTag(airAttackPosTag);

        StartCoroutine(ActionRoutine());
    }

    public int GetAttackDamage()
    {
        return damage;
    }

    void Update()
    {
        // Check if the boss is grounded using Collider2D's IsTouchingLayers method
        bool wasGrounded = isGrounded;
        isGrounded = col.IsTouchingLayers(ground);

        if (isGrounded && !wasGrounded)
        {
            // Reset horizontal velocity when landing
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        // Flip the boss to face the player
        FlipTowardsPlayer();

        // Walk towards the player continuously if not performing an action
        if (isGrounded && !isPerformingAction)
        {
            WalkTowardsPlayer();
        }
    }

    IEnumerator ActionRoutine()
    {
        while (true)
        {
            if (isGrounded)
            {
                float randomValue = Random.value;

                if (randomValue < teleportChance)
                {
                    isPerformingAction = true;
                    TeleportToAttackLocation();
                }
                else if (randomValue < teleportChance + airAttackChance)
                {
                    isPerformingAction = true;
                    PerformAirAttack();
                }
                else
                {
                    isPerformingAction = false;
                }
            }
            yield return new WaitForSeconds(actionInterval);
        }
    }

    void TeleportToAttackLocation()
    {
        if (bossPhaseTwoAttackLocation != null)
        {
            StartCoroutine(TeleportAfterAnimation());
        }
    }

    IEnumerator TeleportAfterAnimation()
    {
        // Set the teleport animation bool to true
        animator.SetBool("EnrollmentBossAir", true);

        // Wait until the teleport animation is done
        yield return new WaitForSeconds(0.3f);
 
        // Teleport the boss to the attack location
        transform.position = bossPhaseTwoAttackLocation.position;

        // Reset the teleport animation bool
        animator.SetBool("EnrollmentBossAir", false);

        isPerformingAction = false; // Reset action flag after teleporting
    }

    void PerformAirAttack()
    {
        StartCoroutine(AirAttackAfterAnimation());
    }

    IEnumerator AirAttackAfterAnimation()
    {
        // Set the air attack animation bool to true
        animator.SetBool("EnrollmentBossGround", true);

        // Wait until the air attack animation is done
        yield return new WaitForSeconds(0.45f);

        if (airAttacks.Length > 0 && airAttackPos.Length > 0)
        {
            List<GameObject> usedSpawnPoints = new List<GameObject>(); // To keep track of used spawn points

            // Shuffle the air attack positions array to randomize the order of selection
            ShuffleArray(airAttackPos);

            foreach (GameObject spawnPoint in airAttackPos)
            {
                if (!usedSpawnPoints.Contains(spawnPoint))
                {
                    // Choose a random air attack prefab
                    GameObject airAttackPrefab = airAttacks[Random.Range(0, airAttacks.Length)];

                    // Instantiate the air attack prefab at the air attack position
                    GameObject airAttack = Instantiate(airAttackPrefab, spawnPoint.transform.position, Quaternion.identity);

                    // Set the lifetime of the air attack
                    Destroy(airAttack, 1f); // Destroy after 3 seconds

                    // Add the used spawn point to the list
                    usedSpawnPoints.Add(spawnPoint);
                }
            }
        }

        // Reset the air attack animation bool
        animator.SetBool("EnrollmentBossGround", false);

        isPerformingAction = false; // Reset action flag after performing air attack
    }

    void ShuffleArray(GameObject[] array)
    {
        // Fisher-Yates shuffle algorithm
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            GameObject temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }

    void WalkTowardsPlayer()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            Vector2 walkDirection = new Vector2(direction.x, 0).normalized; // Only horizontal component for walking

            // Apply walk movement
            rb.velocity = new Vector2(walkDirection.x * walkSpeed, rb.velocity.y);
        }
    }

    void FlipTowardsPlayer()
    {
        if (player != null)
        {
            // Check the direction to the player
            if (player.position.x < transform.position.x && facingRight)
            {
                Flip();
            }
            else if (player.position.x > transform.position.x && !facingRight)
            {
                Flip();
            }
        }
    }

    void Flip()
    {
        // Switch the way the boss is labelled as facing
        facingRight = !facingRight;

        // Multiply the boss's x local scale by -1 to flip its direction
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
