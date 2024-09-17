using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AcademyOutskirtsScript : MonoBehaviour
{
    public GameObject eiraSay;
    private BoxCollider2D boxCollider2D;

    private void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            boxCollider2D.enabled = false;
            StartCoroutine(test());
        }
    }

    IEnumerator test()
    {
        yield return new WaitForSeconds(2);
        eiraSay.SetActive(true);
        yield return new WaitForSeconds(2);
        eiraSay.SetActive(false);
        Destroy(gameObject);
    }
}