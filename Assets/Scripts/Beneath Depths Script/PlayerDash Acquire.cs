using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDashAcquire : MonoBehaviour
{
    public Button acquireButton;

    public Button playerdashButton;

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
        playerdashButton.gameObject.SetActive(true);
        Destroy(gameObject);
    }

}
