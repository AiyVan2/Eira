using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    public float speed = 5f;            // Speed of the homing projectile
    public float rotateSpeed = 200f;    // Rotation speed to turn towards the player
    public float lifetime = 5f;         // Lifetime of the projectile before it self-destructs
    private Transform target;           // The target (player)

    public void Initialize(Transform playerTarget, float projectileSpeed)
    {
        target = playerTarget;
        speed = projectileSpeed;

        // Destroy the projectile after a set lifetime
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        if (target == null) return; // If there's no target, do nothing

        // Calculate direction towards the player
        Vector2 direction = (Vector2)target.position - (Vector2)transform.position;
        direction.Normalize();

        // Rotate the projectile towards the player
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        transform.Rotate(0, 0, -rotateAmount * rotateSpeed * Time.deltaTime);

        // Move forward
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the projectile hits the player or player's attack
        if (collision.CompareTag("Player") || collision.CompareTag("PlayerAttack"))
        {
            // Handle destruction (e.g., damage to the player or self-destruction if hit by an attack)
            Destroy(gameObject);
        }
    }
}