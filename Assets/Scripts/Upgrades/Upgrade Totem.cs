using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTotem : MonoBehaviour
{
    public GameObject playerControlls;
    public GameObject pause;
    public GameObject upgradeScreen;

    //Buttons
    public Button showupgradescreenButton;
    public Button hideupgradescreenButton;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            showupgradescreenButton.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            showupgradescreenButton.gameObject.SetActive(false);
        }
    }


    public void showUpgradeScreen()
    {
        playerControlls.gameObject.SetActive(false);
        pause.SetActive(false);
        upgradeScreen.gameObject.SetActive(true);
        hideupgradescreenButton.gameObject.SetActive(true);
    }
    public void hideUpgradeScreen()
    {
        playerControlls.gameObject.SetActive(true);
        pause.SetActive(true);
        upgradeScreen.gameObject.SetActive(false);
        hideupgradescreenButton.gameObject.SetActive(false);
    }
}
