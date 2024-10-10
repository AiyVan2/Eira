using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed;
    public float JumpForce;
    public GameObject MechanicAttackPrefab;
    public GameObject ScholarAttackPrefab;
    public LayerMask ground;
    public float AttackLifetime;
    public float AttackCooldown;
    public ParticleSystem dust;
    public float dashSpeed;
    public float dashLifetime;
    public float dashCooldown = 5f;
    //public float mana;
    //public float maxMana; 
    //public Slider playerMana; 
    public int scholarAttackManaCost;


    private bool isDashing = false;
    private bool canDash = true;
    private bool canAttack = true;
    private bool canDoubleJump = true;
    private int doubleJumpCount = 0;

    private Rigidbody2D rb;
    private Collider2D col;
    private Animator anim;

    bool isRight;
    bool isLeft;
    bool isUp;
    bool isGround;
    bool isFacingLeft;

    private float lastAttackTime;
    private int attackCount;
    public float comboDelay = 3f;

    public float groundCheckDistance = 1f;

    public Slider dashSlider;

    // Mode Enum for Mechanic and Scholar Mode
    public enum AttackMode { Mechanic, Scholar }
    public AttackMode currentMode = AttackMode.Mechanic;

    private bool canChangeMode = true;
    public float modeChangeCooldown = 2f;


    public AudioManager audioManager;

    private PlayerHealth playerMana;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        playerMana = GetComponent<PlayerHealth>();

        dashSlider.maxValue = dashCooldown;
        dashSlider.value = dashCooldown;
        //playerMana.maxValue = maxMana;
        //playerMana.value = mana;
    }

    void Update()
    {
        UpdateGroundedState();
        //mana = playerMana.value;
        if (isDashing)
        {
            anim.SetBool("isDashing", true);
            anim.SetBool("isRunning", false);
            anim.SetBool("isJumping", false);
            return;
        }
        else
        {
            HandleMovement();
        }

        if (!dashSlider && canDash)
        {
            dashSlider.value += Time.deltaTime;
        }
    }

    void HandleMovement()
    {
        if (!isGround)
        {
            anim.SetBool("isJumping", true);
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isJumping", false);
        }

        if (isRight)
        {
            transform.Translate(transform.right * 1 * MoveSpeed * Time.deltaTime);
            if (isGround)
            {
                anim.SetBool("isRunning", true);
            }
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (isLeft)
        {
            transform.Translate(transform.right * -1 * MoveSpeed * Time.deltaTime);
            if (isGround)
            {
                anim.SetBool("isRunning", true);
            }
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }

    void UpdateGroundedState()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, ground);
        isGround = hit.collider != null;
    }

    public void leftButton()
    {
        isLeft = true;
        isFacingLeft = true;
        isRight = false;
        isUp = false;
    }

    public void rightButton()
    {
        isLeft = false;
        isFacingLeft = false;
        isRight = true;
        isUp = true;
    }

    public void upButton()
    {
        isLeft = false;
        isRight = false;
        isUp = true;
    }

    public void jumpButton()
    {
        if (isGround)
        {
            rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            Dust();
            canDoubleJump = true;
            doubleJumpCount = 0;
            audioManager.PlayJumpSound();
        }
        else if (!isGround && canDoubleJump)
        {
            if (doubleJumpCount < 1)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
                Dust();
                doubleJumpCount++;
                audioManager.PlayJumpSound();
                canDoubleJump = false;

            }
        }
    }

    // Mechanic Attack Method
    public void MechanicAttackButton()
    {
        if (canAttack && !isDashing && currentMode == AttackMode.Mechanic)
        {
            lastAttackTime = Time.time;
            attackCount++;

            if (attackCount == 1)
            {
                anim.SetBool("isAttacking2", true);
                SpawnMechanicProjectileShort();
                StartCoroutine(ResetAttackAnimation("isAttacking2"));
            }
            else if (attackCount == 2)
            {
                anim.SetBool("isAttacking", true);
                SpawnMechanicProjectileLong();
                StartCoroutine(ResetAttackAnimation("isAttacking"));
                attackCount = 0;
            }
            audioManager.PlayAttackSound();
            StartCoroutine(AttackCooldownCoroutine());
        }
    }

    // Scholar Attack Method
    public void ScholarAttackButton()
    {
        if (playerMana.currentMana >= scholarAttackManaCost) // Check if the player has enough mana
        {
            // Deduct mana cost
            playerMana.DeductMana(scholarAttackManaCost);

            // Perform the attack
            anim.SetBool("isAttacking2", true);
            SpawnScholarProjectile();
            StartCoroutine(ResetAttackAnimation("isAttacking2"));
            audioManager.PlayRangeSound();
            // Start cooldown for the next attack
            StartCoroutine(AttackCooldownCoroutine());
        }
        else
        {
            Debug.Log("Not enough mana to perform Scholar attack");
            // Optional: Add feedback for insufficient mana, such as a sound or visual cue
        }
        playerMana.SaveMana();
    }


    void SpawnMechanicProjectileLong()
    {
        Vector3 attackPosition = transform.position + transform.right * 3f;
        GameObject projectile = Instantiate(MechanicAttackPrefab, attackPosition, Quaternion.identity);

        if (isFacingLeft)
        {
            projectile.transform.Rotate(0, 180, 0);
        }
        Destroy(projectile, AttackLifetime);
    }

    void SpawnMechanicProjectileShort()
    {
        Vector3 attackPosition = transform.position + transform.right * 3f;
        GameObject projectile = Instantiate(MechanicAttackPrefab, attackPosition, Quaternion.identity);

        if (isFacingLeft)
        {
            projectile.transform.Rotate(0, 180, 0);
        }
        Destroy(projectile, AttackLifetime);
    }

    void SpawnScholarProjectile()
    {
        Vector3 attackPosition = transform.position + transform.right * 1f;
        GameObject projectile = Instantiate(ScholarAttackPrefab, attackPosition, Quaternion.identity);

        if (isFacingLeft)
        {
            projectile.transform.Rotate(0, 180, 0);
        }
        Destroy(projectile, AttackLifetime);
    }

    private IEnumerator ResetAttackAnimation(string attackBool)
    {
        yield return new WaitForSeconds(0.1f);
        anim.SetBool(attackBool, false);
    }

    private IEnumerator AttackCooldownCoroutine()
    {
        canAttack = false;
        yield return new WaitForSeconds(AttackCooldown);
        canAttack = true;
    }

    public void dashButton()
    {
        if (canDash)
        {
            audioManager.PlayDashSound();
            StartCoroutine(DashCoroutine());
            canDash = false;
            StartCoroutine(FillDashSlider());
            Invoke(nameof(EnableDash), dashCooldown);
        }
    }

    private IEnumerator DashCoroutine()
    {
        isDashing = true;
        float originalMoveSpeed = MoveSpeed;
        float originalGravityScale = rb.gravityScale;
        gameObject.layer = LayerMask.NameToLayer("Dash");

        MoveSpeed = dashSpeed;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(transform.right.x * dashSpeed, 0);

        anim.SetBool("isDashing", true);

        yield return new WaitForSeconds(dashLifetime);

        rb.velocity = Vector2.zero;

        MoveSpeed = originalMoveSpeed;
        rb.gravityScale = originalGravityScale;
        gameObject.layer = 0;
        isDashing = false;
        anim.SetBool("isDashing", false);
    }

    private void EnableDash()
    {
        canDash = true;
        dashSlider.value = dashCooldown;
    }

    // Player Dust Particles
    public void Dust()
    {
        dust.Play();
    }

    private IEnumerator FillDashSlider()
    {
        float timeElapsed = 0;
        while (timeElapsed < dashCooldown)
        {
            dashSlider.value = timeElapsed;
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        dashSlider.value = dashCooldown;
    }
}