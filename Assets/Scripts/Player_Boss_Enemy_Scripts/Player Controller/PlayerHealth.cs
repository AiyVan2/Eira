using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        if (collision.gameObject.CompareTag("Boss")&& !isInvincible)
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
            TakeDamage(100, collision.transform);
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
        
        //Player Push Back On hit
        Vector2 pushbackDirection = (transform.position - damageSource.position).normalized;
        float pushbackForce = 10f; 
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        rb.AddForce(pushbackDirection * pushbackForce, ForceMode2D.Impulse);


        StartCoroutine(timeStop());
        Camera.main.GetComponent<CameraShake>().TriggerShake();
        if (currentHealth <= 0)
        {
            Die();
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
        currentHealth = playerHealth;
        healthSlider.value = currentHealth;

        player.gameObject.SetActive(true);

        Time.timeScale = 1;
    }
    private IEnumerator timeStop()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 1f;
    }
}