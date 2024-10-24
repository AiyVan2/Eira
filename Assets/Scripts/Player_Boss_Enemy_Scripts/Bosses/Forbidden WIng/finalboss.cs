using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class finalboss : MonoBehaviour
{
    public Transform centerPoint;          // Center of the arena
    public Transform leftSpawnLocation;    // Left side of the arena for Attack 2
    public Transform rightSpawnLocation;   // Right side of the arena for Attack 2
    public float moveSpeed = 5f;           // Speed at which the boss moves
    public float chargeSpeed = 7f;         // Speed for charging towards the player

    //Attack Prefab
    public GameObject attackPrefab;
    private float attackLifetime = 0.5f;
    //Attack 1 Locations
    public Transform leftSpawnPoint;       
    public Transform rightSpawnPoint;

    //Attack 3 Locations
    public Transform leftAttack3SpawnPoint;
    public Transform rightAttack3SpawnPoint;
    public Transform middleAttack3SpawnPoint;    

    private Animator animator;
    private bool isVanishing = false;
    private bool canAttack = true;
    private Transform player;
    private Vector3 chargeDirection;
    private bool isFlipped = false;  // Flag to track if the sprite is flipped
    private BossHealth bossHealth;

    //Audio
    public AudioManager audioManager;

    //Ending Text
    public GameObject endingChoices;
    public GameObject endingChoicesText;

    private bool stopSpawning = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        bossHealth = GetComponent<BossHealth>();
    }

    void Update()
    {
        if(bossHealth.health >= 800)
        {
            if (canAttack && !isVanishing)
            {
                StartCoroutine(AttackCycle());
            }
        } 
        else /*(bossHealth.health < 800)*/
        {
            if(canAttack && !isVanishing)
            {
                StartCoroutine(AttackCycleSecondPhase());
            }
        }
        
        // Umbra Solis Death
        if(bossHealth.health <= 0)
        {
            stopSpawning = true;
            StopCoroutine(AttackCycle());
            StopCoroutine(AttackCycleSecondPhase());
            StopCoroutine(ChargeInStraightLine());
            StopCoroutine(VanishAndMoveToCenter());
            StopCoroutine(VanishAndReappearForAttack2());
            StartCoroutine(UmbraSolisDeath());
        }
      
    }

    IEnumerator AttackCycle()
    {
        canAttack = false;

        // Idle before Attack 1
        animator.SetBool("idle", true);
        yield return new WaitForSeconds(0.5f);

        // Attack 1 (Wide attack - spawn 2 prefabs)
        animator.SetBool("Attack1", true);
        animator.SetBool("idle", false);
        audioManager.PlayUmbraSolisFirstAttackSound();
        yield return new WaitForSeconds(0.2f);
        audioManager.PlayUmbraSolisFirstAttackSound();
        SpawnAttackPrefabs(2); // Spawns 2 prefabs on left and right
        yield return new WaitForSeconds(1.2f);
        animator.SetBool("Attack1", false);


        // Attack 2 (Vanishing Charge Attack)
        yield return StartCoroutine(VanishAndReappearForAttack2());
        yield return StartCoroutine(ChargeInStraightLine());
        animator.SetBool("Attack2", false);
        animator.SetBool("idle", true);
        yield return new WaitForSeconds(1f);

        canAttack = true;

        //// Attack 3 (Wider attack - spawn 3 prefabs)
        //animator.SetBool("idle", false);
        //animator.SetBool("Attack3", true);
        //yield return new WaitForSeconds(0.3f); // Timing to match animation
        //SpawnAttackPrefabs(3); // Spawns 3 prefabs for wider attack
        //yield return new WaitForSeconds(2f);
        //animator.SetBool("Attack3", false);
        //animator.SetBool("idle", true);

        //// Vanish and move to center
        //isVanishing = true;
        //animator.SetBool("Attack3", false);
        //animator.SetBool("idle", false);
        //yield return StartCoroutine(VanishAndMoveToCenter());
        //yield return new WaitForSeconds(3f);
    }
    IEnumerator AttackCycleSecondPhase()
    {
        canAttack = false;

        // Idle before Attack 1
        animator.SetBool("idle", true);
        yield return new WaitForSeconds(0.5f);

        // Attack 1 (Wide attack - spawn 2 prefabs)
        animator.SetBool("Attack1", true);
        animator.SetBool("idle", false);
        
        audioManager.PlayUmbraSolisFirstAttackSound();
        yield return new WaitForSeconds(0.2f);
        audioManager.PlayUmbraSolisFirstAttackSound();
        SpawnAttackPrefabs(2); // Spawns 2 prefabs on left and right
        yield return new WaitForSeconds(1.2f);
        animator.SetBool("Attack1", false);


        // Attack 2 (Vanishing Charge Attack)
        yield return StartCoroutine(VanishAndReappearForAttack2());
        yield return StartCoroutine(ChargeInStraightLine());
        animator.SetBool("Attack2", false);
        animator.SetBool("idle", true);
        yield return new WaitForSeconds(1f);

        // Attack 3 (Wider attack - spawn 3 prefabs)
        animator.SetBool("idle", false);
        animator.SetBool("Attack3", true);
        audioManager.PlayUmbraSolisThirdAttackImpactSound();
        yield return new WaitForSeconds(0.3f);
        SpawnAttackPrefabs(1);
        yield return new WaitForSeconds(1f); // Timing to match animation
        audioManager.PlayUmbraSolisThirdAttackSwordSound();
        SpawnAttackPrefabs(3); // Spawns 3 prefabs for wider attack
        yield return new WaitForSeconds(0.2f);
        audioManager.PlayUmbraSolisThirdAttackSwordSound();
        yield return new WaitForSeconds(1f);
        animator.SetBool("Attack3", false);
        animator.SetBool("idle", true);
        yield return new WaitForSeconds(1f);

        // Vanish and move to center
        isVanishing = true;
        animator.SetBool("Attack3", false);
        animator.SetBool("idle", false);
        yield return StartCoroutine(VanishAndMoveToCenter());
        yield return new WaitForSeconds(1f);
    }

    void SpawnAttackPrefabs(int numberOfPrefabs)
    {
        if (stopSpawning) return;

        if (numberOfPrefabs == 2)
        {
            GameObject leftattack = Instantiate(attackPrefab, leftSpawnPoint.position, Quaternion.identity);
            GameObject rightattack = Instantiate(attackPrefab, rightSpawnPoint.position, Quaternion.identity);
            Destroy(leftattack, attackLifetime);
            Destroy(rightattack, attackLifetime);
        }
        else if(numberOfPrefabs == 1)
        {
            GameObject middleattack3 = Instantiate(attackPrefab, middleAttack3SpawnPoint.position, Quaternion.identity);
            Destroy(middleattack3, attackLifetime);
        }
        else if (numberOfPrefabs == 3)
        {
            GameObject leftattack3 = Instantiate(attackPrefab, leftAttack3SpawnPoint.position, Quaternion.identity);
            //GameObject middleattack3 = Instantiate(attackPrefab, middleAttack3SpawnPoint.position, Quaternion.identity);
            GameObject rightattack3 = Instantiate(attackPrefab, rightAttack3SpawnPoint.position, Quaternion.identity);
            Destroy(leftattack3, attackLifetime);
            //Destroy(middleattack3, attackLifetime);
             Destroy(rightattack3 , attackLifetime);
        }
    }

    IEnumerator VanishAndReappearForAttack2()
    {
        // Trigger vanish animation before disappearing
        animator.SetBool("Vanish", true);
        audioManager.PlayUmbraSolisVanishSound();
        yield return new WaitForSeconds(0.1f); // Adjust based on vanish animation length

        // Randomly pick left or right spawn location
        Transform reappearPosition = Random.Range(0, 2) == 0 ? leftSpawnLocation : rightSpawnLocation;

        // Flip the sprite depending on the side the boss teleports to
        if (reappearPosition == rightSpawnLocation && !isFlipped)
        {
            // Flip the sprite to face right
            transform.localScale = new Vector3(-1, 1, 1); // Adjust based on your sprite's original scale
            isFlipped = true;
        }
        else if (reappearPosition == leftSpawnLocation && isFlipped)
        {
            // Flip the sprite to face left (default)
            transform.localScale = new Vector3(1, 1, 1);
            isFlipped = false;
        }
        // End vanish state
        animator.SetBool("Vanish", false);

        // Move boss to chosen position
        transform.position = reappearPosition.position;
        yield return new WaitForSeconds(0.1f); // Time delay before charge begins

       
    }

    IEnumerator ChargeInStraightLine()
    {
        animator.SetBool("Attack2", true);
        audioManager.PlayUmbraSolisSecondAttackSound();
        // Determine charge direction (left-to-right or right-to-left)
        chargeDirection = transform.position.x < player.position.x ? Vector3.right : Vector3.left;

        // Charge in a straight line for a set distance or time
        float chargeTime = 2f; // Adjust as needed for distance
        float elapsedTime = 0f;

        while (elapsedTime < chargeTime)
        {
            transform.position += chargeDirection * chargeSpeed * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator VanishAndMoveToCenter()
    {
        animator.SetBool("Vanish", true);
        audioManager.PlayUmbraSolisVanishSound();
        yield return new WaitForSeconds(0.18f);
        // Teleport the boss to the center point
        transform.position = centerPoint.position;

        // Reset vanish state and allow the boss to attack again
        isVanishing = false;
        canAttack = true;

      
        animator.SetBool("Vanish", false);
    }
    IEnumerator UmbraSolisDeath()
    {
        animator.SetBool("idle", false);
        animator.SetBool("Attack1", false);
        animator.SetBool("Attack2", false);
        animator.SetBool("Attack3", false);
        animator.SetBool("Vanish", false);
        animator.SetBool("Death", true);
        yield return new WaitForSeconds(2.3f);
        endingChoices.SetActive(true);
        endingChoicesText.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}