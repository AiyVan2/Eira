using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWaySwitch : MonoBehaviour
{
    public OneWayPlatform onewayPlatform;
    public Animator leverAnimator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            leverAnimator.SetBool("Flipped1", true);
            onewayPlatform.StartElevator();
        }
    }
}
