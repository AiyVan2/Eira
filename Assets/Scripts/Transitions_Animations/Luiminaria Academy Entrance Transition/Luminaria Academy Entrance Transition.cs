using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LuminariaAcademyEntranceTransition : MonoBehaviour
{
    public Animator transitionanim;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(OutskirtstoHallofKnowledge());
        }
    }
    IEnumerator OutskirtstoHallofKnowledge()
    {
        transitionanim.SetTrigger("End");
        yield return new WaitForSeconds(transitionanim.GetCurrentAnimatorStateInfo(0).length);
        SceneManager.LoadScene(10);
        yield return null;
    }
}

