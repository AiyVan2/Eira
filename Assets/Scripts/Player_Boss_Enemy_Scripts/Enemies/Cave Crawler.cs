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

    // Raycast length for ground detection
    public float groundCheckDistance = 0.5f;
    public float groundCheckOffset = 0.5f; // Offset from the center to the front'

    // Cooldown time for flipping
    public float flipCooldown = 0.5f;
    private float lastFlipTime;

    // Update is called once per frame
    void Update()
    {
        // Move the enemy
        Move();
    }

    void Move()
    {
        // Check if the enemy is grounded using raycast
        bool isGrounded = IsGrounded();

        // If the enemy is grounded, move forward
        if (isGrounded)
        {
            transform.position += new Vector3(speed * direction * Time.deltaTime, 0, 0);
        }
        else
        {
            // Only flip if enough time has passed since the last flip
            if (Time.time >= lastFlipTime + flipCooldown)
            {
                Flip();
                lastFlipTime = Time.time;
            }
        }
    }

    bool IsGrounded()
    {
        // Calculate the position slightly ahead of the crawler in the direction it's facing
        Vector2 raycastOrigin = (Vector2)transform.position + Vector2.right * direction * groundCheckOffset;

        // Perform a raycast downwards from the calculated position
        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, Vector2.down, groundCheckDistance, groundLayer);

        return hit.collider != null;
    }

    void Flip()
    {
        // Flip the movement direction
        direction *= -1;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    // Debugging purposes to visualize the raycast
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
    }
}