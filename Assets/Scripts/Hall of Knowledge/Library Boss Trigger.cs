using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraryBossTrigger : MonoBehaviour
{
    public GameObject libraryBoss;
    public GameObject bossareaBarier;

    public GameObject readButton;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            readButton.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            readButton.gameObject.SetActive(false);
        }
    }


    public void bossFight()
    {
        libraryBoss.SetActive(true);
        bossareaBarier.SetActive(true);
    }

}
