using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthSlider;
    public GameObject player;
    public int maxHealth = 100;
    public int currentHealth;
    private SpriteRenderer spriteRenderer;
    private bool isInvincible = false;
    private float invincibilityDuration = 2f;
    private Color originalColor;

    public Button restart;

    // Reference to PlayerMovement to access mana
    private PlayerMovement playerMovement;
    public int healAmount = 15; // The amount of health to restore per heal
    public int maxMana = 50; // Maximum mana
    public int currentMana; // Current mana
    public int healManaCost = 25; // Mana cost for healing
    public Slider playerMana;
    private Animator animator;
    void Start()
    {
        // Load saved health from PlayerPrefs or set it to max health
        if (PlayerPrefs.HasKey("PlayerHealth"))
        {
            currentHealth = PlayerPrefs.GetInt("PlayerHealth");
        }
        else
        {
            currentHealth = maxHealth;
        }


        if(PlayerPrefs.HasKey("Player Mana"))
        {
           currentMana = PlayerPrefs.GetInt("Player Mana");
        }
        else
        {
            currentMana = maxMana;
        }
        // Ensure health is within valid range
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        playerMana.maxValue = maxMana;
        playerMana.value = currentMana;

        // Set up other components
        spriteRenderer = player.GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        playerMovement = player.GetComponent<PlayerMovement>();
        animator = player.GetComponent<Animator>();

        // Ensure health is reset to max if starting a new game
        if (SceneManager.GetActiveScene().name == "Eira Beginning") // Adjust scene name accordingly
        {
            ResetHealth();
            ResetMana();
        }
    }
    private void Update()
    {
        playerMana.value = currentMana;
    }
    public void SaveHealth()
    {
        PlayerPrefs.SetInt("PlayerHealth", currentHealth);
        PlayerPrefs.Save(); // Ensure the health is saved
        Debug.Log("Saving Health");
    }
    public void ResetHealth()
    {
        currentHealth = maxHealth;
        healthSlider.value = currentHealth;
        SaveHealth(); // Optional: Save health to PlayerPrefs if you want to preserve it
    }
    public void SaveMana()
    {
        PlayerPrefs.SetInt("Player Mana", currentMana);
        PlayerPrefs.Save();
        Debug.Log("Saving mana");
    }
    public void ResetMana()
    {
      currentMana = maxMana;
      playerMana.value = currentMana;
      SaveMana();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isInvincible)
        {
            EnemyDamage enemy = collision.gameObject.GetComponent<EnemyDamage>();
            int enemyDamage = enemy.GetEnemyDamage();
            TakeDamage(enemyDamage, collision.transform);
            Debug.Log("Hitting the plyaer");

        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isInvincible)
        {
            EnemyDamage enemy = collision.gameObject.GetComponent<EnemyDamage>();
            int enemyDamage = enemy.GetEnemyDamage();
            TakeDamage(enemyDamage, collision.transform);
            Debug.Log("Player is inside the enemy's collider.");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boss") && !isInvincible)
        {
            EnemyDamage enemy = collision.gameObject.GetComponent<EnemyDamage>();
            int enemyDamage = enemy.GetEnemyDamage();
            TakeDamage(enemyDamage, collision.transform);
        }
        else if (collision.gameObject.CompareTag("Forest Guardian Ground Attack"))
        {
            TakeDamage(40, collision.transform);
        }
        if (collision.gameObject.CompareTag("Traps"))
        {
            TakeDamage(10, collision.transform);
            trapdamageHandler();
        }
    }




    public void TakeDamage(int damage, Transform damageSource)
    {
        currentHealth -= damage;
        healthSlider.value = currentHealth;

        // Player Push Back On hit
        Vector2 pushbackDirection = (transform.position - damageSource.position).normalized;

        // Adjust pushback force and direction based on collision type
        float pushbackForce = 7f; // Lowered the pushback force for smoother feel
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

        // Apply pushback
        rb.velocity = Vector2.zero; // Reset velocity before applying force
        rb.AddForce(pushbackDirection * pushbackForce, ForceMode2D.Impulse);

        // Cap the pushback velocity to avoid excessive movement
        float maxPushbackVelocity = 5f;
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxPushbackVelocity);

        //Time Scale half when getting hit
        if (currentHealth > 0)
        {
            StartCoroutine(timeStop());
        }

        Camera.main.GetComponent<CameraShake>().TriggerShake();

        if (currentHealth <= 0)
        {
            Time.timeScale = 0f;
            restart.gameObject.SetActive(true);
            return; // Exit early if the player is dead
        }

        StartCoroutine(BlinkAndInvincible());
        SaveHealth();
    }

    private IEnumerator BlinkAndInvincible()
    {
        isInvincible = true;
        float blinkDuration = invincibilityDuration / 10f;
        int originalLayer = player.layer;
        player.layer = LayerMask.NameToLayer("Untargetable");
        for (int i = 0; i < 10; i++)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(blinkDuration / 2);
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(blinkDuration / 2);
        }
        player.layer = originalLayer;
        isInvincible = false;
    }

    private void trapdamageHandler()
    {
        // Stop the BlinkAndInvincible coroutine
        StopCoroutine(BlinkAndInvincible());
        // Reset player to a saved position or respawn point
        float playerX = PlayerPrefs.GetFloat("PlayerX");
        float playerY = PlayerPrefs.GetFloat("PlayerY");
        float playerZ = PlayerPrefs.GetFloat("PlayerZ");

        // Reset the player's position
        player.transform.position = new Vector3(playerX, playerY, playerZ);
        player.layer = LayerMask.NameToLayer("Player"); // or whatever the original layer is
        isInvincible = false;
        // Start the BlinkAndInvincible coroutine again
        StartCoroutine(BlinkAndInvincible());

    }
    private IEnumerator timeStop()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSecondsRealtime(0.5f);

        // Only reset time scale if the player is still alive
        if (currentHealth > 0)
        {
            Time.timeScale = 1f;
        }
    }

    
    public void Heal()
    { 
        if (isInvincible)
        {
            Debug.Log("Cannot Heal");
        }
        else
        {
            StartCoroutine(HealPlayer());
        }
        
    }

    // Healing method using mana
    private IEnumerator HealPlayer()
    {
        if (currentMana >= healManaCost && currentHealth < maxHealth)
        {
            // Deduct mana
           currentMana -= (int)healManaCost;
            SaveMana(); 
           // Update mana slider (if you have one)
           // ...

           animator.SetBool("isHealing", true);
            yield return new WaitForSeconds(0.13f);
            // Heal the player
            currentHealth = Mathf.Min(currentHealth + (int)healAmount, maxHealth);
            animator.SetBool("isHealing", false);
            healthSlider.value = currentHealth;

            // Optional: Play healing animation or sound effect
            Debug.Log("Player healed by " + healAmount);
        }
        else
        {
            Debug.Log("Not enough mana or health is already full");
        }
        SaveHealth();
    }
    // Method to deduct mana
    public void DeductMana(int amount)
    {
        currentMana -= amount;
        playerMana.value = currentMana;
        SaveMana();
    }

    // Method to add mana
    public void AddMana(int amount)
    {
        currentMana = Mathf.Min(currentMana + amount, maxMana);
        playerMana.value = currentMana;
        SaveMana();
    }
}