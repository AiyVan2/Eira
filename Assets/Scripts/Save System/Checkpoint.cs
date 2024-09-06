using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checkpoint : MonoBehaviour
{
    public Button saveButton;
    //private SaveManager saveManager;

    //private void Start()
    //{
    //    saveManager = FindObjectOfType<SaveManager>();
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            saveButton.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            saveButton.gameObject.SetActive(false);
        }
    }
}