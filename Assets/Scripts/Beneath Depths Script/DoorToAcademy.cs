using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorToAcademy : MonoBehaviour
{
    public GameObject Door;
    public GameObject Barrier;
    public Text hintText;

    private PlayerInventory playerInventory;
    private Animator animator;

    private void Start()
    {
        playerInventory = GameObject.FindWithTag("Player").GetComponent<PlayerInventory>();
        animator = Door.GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerInventory.hasbeneathDepthsKey)
            {
                Barrier.SetActive(false);
                animator.SetTrigger("DoorOpen");
                hintText.gameObject.SetActive(false);
            }
            else 
            {
                hintText.gameObject.SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
        
                hintText.gameObject.SetActive(false);
        }
    }
}
