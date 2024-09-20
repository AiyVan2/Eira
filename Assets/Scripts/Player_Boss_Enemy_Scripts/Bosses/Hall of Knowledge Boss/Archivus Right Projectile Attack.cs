using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchivusRightProjectileAttack : MonoBehaviour
{
    public float speed;
    public float lifetime;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * -speed;
        Destroy(gameObject, lifetime);
    }
}
