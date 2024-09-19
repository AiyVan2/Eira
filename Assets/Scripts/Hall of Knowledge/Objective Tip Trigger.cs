using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveTipTrigger : MonoBehaviour
{
    public Text objectiveText;
    public GameObject eiraSay;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(text());
        }
    }

    IEnumerator text()
    {
        eiraSay.SetActive(true);
        yield return new WaitForSeconds(3);
        objectiveText.text = "Objective: Find the Librarian Room to uncover a clue about the academy’s fall";
        eiraSay.SetActive(false);
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}
