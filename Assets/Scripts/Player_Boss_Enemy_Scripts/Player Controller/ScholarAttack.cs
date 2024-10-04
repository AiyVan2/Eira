using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScholarAttack : MonoBehaviour
{
    public float speed;
    //public int damage;   
    public float lifetime;

    private GameObject player;
    private Rigidbody2D rb;

    private PlayerStats playerStats;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        playerStats = player.GetComponent<PlayerStats>();
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss")) 
        {
            if(collision.gameObject.TryGetComponent<BossHealth>(out var bossHealth))
            {
                bossHealth.TakeDamage(playerStats.scholarDamage);
            }
            if (collision.gameObject.TryGetComponent<EnemyHealth>(out var enemyHealth))
            {
                enemyHealth.TakeDamage(playerStats.scholarDamage);
            }
          
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject); 
        }
    }
}
