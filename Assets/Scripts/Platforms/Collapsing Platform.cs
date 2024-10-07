using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapsingPlatform : MonoBehaviour
{
    private Vector3 originalPosition; // Store the original position of the platform
    private Collider2D platformCollider; // Reference to the platform's collider
    private SpriteRenderer platformRenderer; // Reference to the platform's visual component

    // Delay before the platform disappears and reappears
    public float collapseDelay = 1.2f;
    public float respawnDelay = 2f;



    private Animator platformAnimator;

    private void Start()
    {
        // Get references to the platform's components
        platformCollider = GetComponent<Collider2D>();
        platformRenderer = GetComponent<SpriteRenderer>();
        platformAnimator = GetComponent<Animator>();
        originalPosition = transform.position;

        platformAnimator.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(CollapsePlatform());
        }
    }

    private IEnumerator CollapsePlatform()
    {

        platformAnimator.enabled = true;
        // Wait for the collapse delay before collapsing the platform
        yield return new WaitForSeconds(collapseDelay);
        platformAnimator.enabled = false;
        // Disable the platform's collider and renderer to simulate collapsing
        platformCollider.enabled = false;
        platformRenderer.enabled = false;

        // Wait for the respawn delay
        yield return new WaitForSeconds(respawnDelay);

        // Re-enable the platform's collider and renderer to simulate reappearing
        platformCollider.enabled = true;
        platformRenderer.enabled = true;

        // Ensure the platform is back at its original position (if needed)
        transform.position = originalPosition;
    }
}
