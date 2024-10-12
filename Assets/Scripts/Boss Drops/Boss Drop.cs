using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDrop : MonoBehaviour
{
    private int bossdropLifetime = 6;

    //Audio
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            playerHealth.currentHealth = playerHealth.maxHealth;
            playerHealth.SaveHealth();
            playerHealth.healthSlider.value = playerHealth.currentHealth;
            audioManager.PlayLuminShardPickupSound();
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, bossdropLifetime);
        }
    }
}
