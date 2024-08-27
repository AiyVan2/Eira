using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AreaAnimations : MonoBehaviour
{
    public enum Detection {Door, BeneathOutpostDoor}
    public Detection detection;
    public Animator animator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            switch (detection)
            {
                case Detection.Door:
                    animator.SetBool("DoorOpen", true);
                    break;
                case Detection.BeneathOutpostDoor:
                    animator.SetBool("DoorOpen", true);
                    break;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            switch (detection)
            {
                case Detection.Door:
                    animator.SetBool("DoorOpen", false);
                    break;
            }
        }
    }





}
