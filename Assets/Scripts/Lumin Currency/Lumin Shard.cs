using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuminShard : MonoBehaviour
{
    // Start is called before the first frame update
    public int value = 1; // Amount of currency this collectible gives

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player collided with the currency collectible
        if (collision.gameObject.CompareTag("Player"))
        {
            // Access the player's currency manager and add value
            PlayerLuminShards playerCurrency = collision.gameObject.GetComponent<PlayerLuminShards>();
            if (playerCurrency != null)
            {
                playerCurrency.AddCurrency(value);
            }

            // Destroy the collectible after it has been picked up
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, 8f);
        }
    }
}

