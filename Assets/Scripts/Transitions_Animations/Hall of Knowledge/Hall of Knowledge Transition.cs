using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HallofKnowledgeTransition : MonoBehaviour
{
    public Animator transitionanim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(hallofknowledgetoastronomytowerTransition());
        }
    }
    IEnumerator hallofknowledgetoastronomytowerTransition()
    {
        transitionanim.SetTrigger("End");
        yield return new WaitForSeconds(transitionanim.GetCurrentAnimatorStateInfo(0).length);
        SceneManager.LoadScene(9);
    }
}
