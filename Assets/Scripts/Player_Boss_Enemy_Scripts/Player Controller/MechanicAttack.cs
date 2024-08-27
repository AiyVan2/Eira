using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicAttack : MonoBehaviour
{
    public float lifetime = 2f;
    public float pushbackForce = 5f;
    public float enemypushbackForce;
    public int damage = 10;
    private Rigidbody2D rb;
    private GameObject player;
    private GameObject boss;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime);
        player = GameObject.FindWithTag("Player");
        boss = GameObject.FindWithTag("Boss");
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MechanicEnemy") || collision.gameObject.CompareTag("Boss"))
        {
            if (collision.gameObject.TryGetComponent<ForestGuardianHealth>(out var bossHealth))
            {
                bossHealth.TakeDamage(damage);
                playerpushBack();
            }
            else if (collision.gameObject.TryGetComponent<EnemyHealth>(out var enemyHealth))
            {
                enemyHealth.TakeDamage(damage);
                enemypushBack(collision.gameObject);
            }

            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

    private void playerpushBack()
    {
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        Rigidbody2D bossrb = boss.GetComponent<Rigidbody2D>();
        Vector2 pushbackDirection = (player.transform.position - boss.transform.position).normalized;
        playerRb.AddForce(pushbackDirection * pushbackForce, ForceMode2D.Impulse);
    }
    private void enemypushBack(GameObject enemy)
    {
        Rigidbody2D enemyrb = enemy.GetComponent<Rigidbody2D>();
        Vector2 pushbackDirection = (enemy.transform.position - player.transform.position).normalized;
        enemyrb.AddForce(pushbackDirection * enemypushbackForce, ForceMode2D.Impulse);
    }
}


