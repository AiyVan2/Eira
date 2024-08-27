using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            PlayerPrefs.SetFloat("PlayerX", collision.transform.position.x);
            PlayerPrefs.SetFloat("PlayerY", collision.transform.position.y);
            PlayerPrefs.SetFloat("PlayerZ", collision.transform.position.z);

            // Get the player's health from the PlayerHealth script
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            PlayerPrefs.SetInt("PlayerHealth", playerHealth.currentHealth);

            // Save the changes
            PlayerPrefs.Save();

            Debug.Log("Player state saved!");
        }
    }
}
