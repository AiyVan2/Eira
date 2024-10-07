using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveTipTrigger : MonoBehaviour
{
    public CameraSwap swap;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(text());
        }
    }

    IEnumerator text()
    {
        swap.swaptoboosRoom();
        yield return new WaitForSeconds(2f);
        swap.returntoPlayerCamera();
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
    }
}
