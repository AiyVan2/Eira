using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScholarAttack : MonoBehaviour
{
    public float speed;
    public int damage;   
    public float lifetime; 

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AncientEnemy") || collision.gameObject.CompareTag("Boss")) 
        {
            if(collision.gameObject.TryGetComponent<BossHealth>(out var bossHealth))
            {
                bossHealth.TakeDamage(damage);
            }
            if (collision.gameObject.TryGetComponent<EnemyHealth>(out var enemyHealth))
            {
                enemyHealth.TakeDamage(damage);
            }
          
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject); 
        }
    }
}
