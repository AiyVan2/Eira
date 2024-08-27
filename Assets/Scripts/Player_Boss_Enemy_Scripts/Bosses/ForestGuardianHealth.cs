using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForestGuardianHealth : MonoBehaviour
{
    public Slider bossHealthSlider;
    public GameObject victorypanel;
    public int health; 
    private SpriteRenderer sr;
    private Color originalColor;
    private Rigidbody2D rb;
    private Animator animator;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        originalColor = sr.color;
        UpdateHealth();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("BossPhaseTwo Took Damage: " + damage);
        UpdateHealth();

        if (health <= 0)
        {
            Debug.Log("BossPhaseTwo defeated");
            StartCoroutine(HandleBossDefeat());
        }
        else
        {
            StartCoroutine(BlinkColor());
        }
    }

    private IEnumerator HandleBossDefeat()
    {
        yield return StartCoroutine(EnrollmentBossDeathAnimation());

        Destroy(gameObject);
        victorypanel.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    private IEnumerator EnrollmentBossDeathAnimation()
    {
        animator.SetBool("EnrollmentBossDeath", true);
        rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX;
        yield return new WaitForSeconds(0.6f);
    }

    void UpdateHealth()
    {
        Debug.Log("BossPhaseTwo Health: " + health);
        bossHealthSlider.value = health;
    }

    private IEnumerator BlinkColor()
    {
        float duration = 0.1f; // Duration of each color change
        float elapsedTime = 0f;
        bool isWhite = true;

        while (elapsedTime < duration) // Blink 5 times
        {
            sr.color = isWhite ? Color.red : originalColor;
            isWhite = !isWhite;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        sr.color = originalColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            // Assuming the damage dealt is 1
            TakeDamage(1);
        }
    }
}
