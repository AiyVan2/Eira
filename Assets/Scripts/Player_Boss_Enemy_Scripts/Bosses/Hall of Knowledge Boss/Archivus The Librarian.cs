using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

public class ArchivusTheLibrarianAI : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float teleportCooldown = 5.0f;
    public float attackCooldown = 1.0f;
    public float idleDuration = 2.0f;
    public GameObject projectileRightPrefab;
    public GameObject projectileLeftPrefab;
    public GameObject homingProjectilePrefab;
    public GameObject attackPrefab; // Prefab for attack
    public Transform[] topAttackLocations; // 6 locations for top attack
    public Transform[] sideAttackLocations; // 3 locations for side attack
    public Transform[] projectileLeftAttackLocations;
    public Transform[] projectileRightAttackLocations;
    public Transform[] homingProjectileAttackLocations;

    public Transform topPosition; // Position where the boss teleports to the top
    public Transform leftPosition; // Position where the boss teleports to the left side
    public Transform rightPosition; // Position where the boss teleports to the right side
    public Transform idlePosition; // Position where the boss idles

    private Transform player;
    private bool isAttacking = false;

    private BossHealth bossHealth;

    public GameObject astronomytowerLever;
    public GameObject bossroomBarrier;
    public GameObject hiddenroomBarrier;
    public CameraSwap swap;


    private Animator animator;
    private SpriteRenderer spriteRenderer;

    //Audio
    public AudioManager audioManager;

    //Background Boss Music
    public GameObject backgroundMusic;


    //background Ambiance Music
    public GameObject backgroundambianceMusic;

    //Boss Drop
    public GameObject bossDrop;



    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        bossHealth = GetComponent<BossHealth>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(BossRoutine());
    }
    private void Update()
    {
    if(bossHealth.health <= 0)
        {
            StartCoroutine(ArchivusDeath());
        }
        FacePlayer();
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
        animator.SetBool("Teleport", true);
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("Teleport", false);

        // Teleport to the new position
        animator.SetBool("Reappear", true);
        transform.position = teleportPosition;
        yield return new WaitForSeconds(0.3f);
        animator.SetBool("Reappear", false);


        if (isTop)
        {
            yield return new WaitForSeconds(2f);


            animator.SetBool("Attack", true);
            audioManager.PlayArchivusAttackSound();
            yield return new WaitForSeconds(0.3f);
            // Spawn attack prefabs at all 6 top locations
            SpawnAttackPrefabs(topAttackLocations);
            if(bossHealth.health < 200)
            {
                SpawnAttackPrefabs(topAttackLocations);
                SpawnHomingProjecitle(homingProjectileAttackLocations);
            }
            animator.SetBool("Attack", false);
        }
        else
        {
            yield return new WaitForSeconds(2f);
            animator.SetBool("Attack", true);
            audioManager.PlayArchivusAttackSound();
            yield return new WaitForSeconds(0.3f);
            // Spawn attack prefabs at 3 side locations
            SpawnAttackPrefabs(sideAttackLocations);

            if(bossHealth.health < 200)
            {
                SpawnAttackPrefabs(sideAttackLocations);
                SpawnRightProjectileAttackPrefabs(projectileRightAttackLocations);
                SpawnLeftProjectileAttackPrefabs(projectileLeftAttackLocations);
            }
            animator.SetBool("Attack", false);
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

    private void SpawnLeftProjectileAttackPrefabs(Transform[] locations)
    {
        foreach (Transform location in locations)
        {
            Instantiate(projectileRightPrefab, location.position, Quaternion.identity);
        }
    }

    private void SpawnRightProjectileAttackPrefabs(Transform[] locations)
    {
        foreach (Transform location in locations)
        {
            Instantiate(projectileLeftPrefab, location.position, Quaternion.identity);
        }
    }

    private void SpawnHomingProjecitle(Transform[] locations)
    {
        int numberofProjectile = 2;
        int projectileSpeed = 5;
        int spreadAngle = 100;
        float angleStep = spreadAngle / (numberofProjectile - 1);


        foreach (Transform location in locations)
        {

            for(int i = 0; i < numberofProjectile; i++ )
            {
                float angle = -spreadAngle / 2 + angleStep * i;

                // Instantiate the homing projectile from the prefab
                GameObject homingProjectile = Instantiate(homingProjectilePrefab, location.position, Quaternion.identity);

                homingProjectile.transform.Rotate(0, 0, angle);

                // Get the HomingProjectile script and initialize it
                HomingProjectile projectileScript = homingProjectile.GetComponent<HomingProjectile>();
                if (projectileScript != null)
                {
                    // Pass the player and speed to the projectile
                    projectileScript.Initialize(player, projectileSpeed);
                }
            }
        }
    }

    private IEnumerator Idle()
    {

        // Idle in the middle
        if (idlePosition != null)
        {

            animator.SetBool("Teleport", true);
            yield return new WaitForSeconds(0.2f);
            animator.SetBool("Teleport", false);
            animator.SetBool("Reappear", true);
            transform.position = idlePosition.position;
            yield return new WaitForSeconds(0.3f);
            animator.SetBool("Reappear", false);
        }
        else
        {
            Debug.LogWarning("Idle position not set for " + gameObject.name);
        }

        yield return new WaitForSeconds(idleDuration);
    }


    private void FacePlayer()
    {
        if (player == null) return;

        Vector3 direction = (player.position - transform.position).normalized;

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

    IEnumerator ArchivusDeath()
    {
        swap.returntoPlayerCamera();
        astronomytowerLever.SetActive(true);
        bossroomBarrier.SetActive(false);
        hiddenroomBarrier.SetActive(false);
        backgroundMusic.SetActive(false);
        backgroundambianceMusic.SetActive(true);
        yield return new WaitForSeconds(1f);
        Instantiate(bossDrop, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}