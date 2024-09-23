using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchivusLeftProjectileAttack : MonoBehaviour
{
    public float speed;
    public float lifetime;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        Destroy(gameObject, lifetime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            Destroy(gameObject);
        }
    }
}
