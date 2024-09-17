using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDashAcquire : MonoBehaviour
{
    public Button acquireButton;
    public Button playerdashButton;

    public GameObject playerdashUI;
    private PlayerInventory playerInventory;  // Reference to PlayerInventory

    private void Start()
    {
        playerInventory = GameObject.FindWithTag("Player").GetComponent<PlayerInventory>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            acquireButton.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            acquireButton.gameObject.SetActive(false);
        }
    }

    public void dashAcquired()
    {
        playerInventory.hasDash = true;
        playerInventory.hasbeneathDepthsKey = true;
        playerdashButton.gameObject.SetActive(true);
        playerdashUI.gameObject.SetActive(true);
        Destroy(gameObject);
    }
}