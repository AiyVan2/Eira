using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AcademyOutskirtsScript : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;

    public CameraSwap swap;

    private void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            boxCollider2D.enabled = false;
            StartCoroutine(LookAtTheAcademy());
        }
    }

    IEnumerator LookAtTheAcademy()
    {
        swap.swaptoboosRoom();
        yield return new WaitForSeconds(5f);
        swap.returntoPlayerCamera();
      
        Destroy(gameObject);
    }
}
