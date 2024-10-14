using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuminCoreDrop : MonoBehaviour
{
    public GameObject luminshardDrop;
    private int health = 3;
    public int spreadForce = 10;

    private SpriteRenderer spriteRenderer;
    //Camera Shake
    private CameraShake shake;

    //Particle
    public ParticleSystem hit;

    //Audio
    public AudioManager audioManager;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {

                 health --;
            StartCoroutine(changeColor());
            hit.Play();
            Camera.main.GetComponent<CameraShake>().TriggerShake();
            if (health <= 0)
            {
                lumindropshardCount();
                audioManager.PlayEnemyDeathSound();
                Destroy(gameObject);
            }
        }
    }

    private void lumindropshardCount()
    {
        int luminshardCount = Random.Range(6, 10);
        for(int i = 0; i < luminshardCount; i++)
        {
            GameObject luminshards = Instantiate(luminshardDrop, transform.position, Quaternion.identity);
            // Calculate direction for the fragment
            Vector2 spreadDirection = Random.insideUnitCircle.normalized; // Random direction
            Rigidbody2D rb = luminshards.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Apply force to the fragment
                rb.AddForce(spreadDirection * spreadForce, ForceMode2D.Impulse);
            }
        }
    }

   IEnumerator changeColor()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }
}
