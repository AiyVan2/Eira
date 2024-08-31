using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

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


    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        spriteRenderer = player.GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boss") && !isInvincible)
        {
            ForestGuardianMovements boss = collision.gameObject.GetComponent<ForestGuardianMovements>();
            int bossDamage = boss.GetAttackDamage();
            TakeDamage(bossDamage, collision.transform);
        }
        else if (collision.gameObject.CompareTag("Forest Guardian Ground Attack"))
        {
            TakeDamage(40, collision.transform);
        }
        if (collision.gameObject.CompareTag("Traps"))
        {
            TakeDamage(10, collision.transform);
            Die();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isInvincible)
        {
            EnemyDamage enemy = collision.gameObject.GetComponent<EnemyDamage>();
            int enemyDamage = enemy.GetEnemyDamage();
            TakeDamage(enemyDamage, collision.transform);

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
    }

    private IEnumerator BlinkAndInvincible()
    {
        isInvincible = true;
        float blinkDuration = invincibilityDuration / 10f;

        for (int i = 0; i < 10; i++)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(blinkDuration / 2);
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(blinkDuration / 2);
        }

        isInvincible = false;
    }
    private void Die()
    {
        // Load the saved player state
        float playerX = PlayerPrefs.GetFloat("PlayerX");
        float playerY = PlayerPrefs.GetFloat("PlayerY");
        float playerZ = PlayerPrefs.GetFloat("PlayerZ");
        int playerHealth = PlayerPrefs.GetInt("PlayerHealth");

        // Reset the player's position and health
        player.transform.position = new Vector3(playerX, playerY, playerZ);

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
}