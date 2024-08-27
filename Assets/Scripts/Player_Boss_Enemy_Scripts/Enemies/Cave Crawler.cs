using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveCrawler : MonoBehaviour
{
    // Movement variables
    public float speed = 2.0f;
    private int direction = 1; // 1 for right, -1 for left

    // Reference to the ground layer
    public LayerMask groundLayer;

    // Reference to the ground check object
    public GameObject groundCheckObject;

    // Update is called once per frame
    void Update()
    {
        // Move the enemy
        Move();
    }

    void Move()
    {
        // Check if the ground check object is colliding with the ground
        bool isGrounded = IsGrounded();

        // If the enemy is grounded, move forward
        if (isGrounded)
        {
            transform.position += new Vector3(speed * direction * Time.deltaTime, 0, 0);
        }
        else
        {
            Flip();
        }
    }

    bool IsGrounded()
    {
        return groundCheckObject.GetComponent<Collider2D>().IsTouchingLayers(groundLayer);
    }

    void Flip()
    {
        direction *= -1; // flip direction
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}