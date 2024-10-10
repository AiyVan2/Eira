using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    public GameObject luminShards;
    private SpriteRenderer spriteRenderer;
    public ParticleSystem hitEffect;


    //Audio
    public AudioManager audioManager;


    //Lumin Shard Drop
    private int luminCount;
    private float spreadForce = 5f; // Define the explosion force value
    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("BossPhaseTwo Took Damage: " + damage);
        StartCoroutine(blink());
        Camera.main.GetComponent<CameraShake>().TriggerShake();
        hitEffect.Play();

        if (health <= 0)
        {
            Debug.Log("BossPhaseTwo defeated");
            audioManager.PlayEnemyDeathSound();
            dropluminShards();
            Destroy(gameObject);
        }
    }
    IEnumerator blink()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.color = Color.white;
    }

    public void dropluminShards()
    {

        luminCount = Random.Range(1, 5);

        for (int i = 0; i < luminCount ; i++)
        {
            // Instantiate a fragment
            GameObject LuminShards = Instantiate(luminShards, transform.position, Quaternion.identity);

            // Calculate direction for the fragment
            Vector2 spreadDirection = Random.insideUnitCircle.normalized; // Random direction
            Rigidbody2D rb = LuminShards.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Apply force to the fragment
                rb.AddForce(spreadDirection * spreadForce, ForceMode2D.Impulse);
            }
        }
    }
}
