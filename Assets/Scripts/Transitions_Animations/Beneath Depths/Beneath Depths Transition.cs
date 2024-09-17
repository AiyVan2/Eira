using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BeneathDepthsTransition : MonoBehaviour
{
    public enum Area { UppertoLower, LowertoAcademy, LowertoBoss, BosstoLower, BeneathDepthstoAcademyOutskirts};
    public Area areas;
    public Animator transitionanim;

    //Player Controlls
    public GameObject PlayerControlls;

    //Areas GameObject
    public GameObject DepthsUpperLevel;
    public GameObject DepthsLowerLevel;
    public GameObject DepthsToAcademy;
    public GameObject DepthsBossRoom;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            switch (areas)
            {
                case Area.UppertoLower:
                    StartCoroutine(UppertoLowerTransition());
                    break;
                case Area.LowertoAcademy:
                    StartCoroutine(LowertoAcademyTransition());
                    break;
                case Area.LowertoBoss:
                    StartCoroutine(LowertoBossRoomTransition());
                    break;
                case Area.BosstoLower:
                    StartCoroutine(BossRoomtoLowerTransition());
                    break;
                case Area.BeneathDepthstoAcademyOutskirts:
                    StartCoroutine(BeneathDepthstoAcademyOutskirtsTransition());
                    break;
            }
        }
    }

    IEnumerator UppertoLowerTransition()
    {
        transitionanim.SetTrigger("End");
        yield return new WaitForSeconds(transitionanim.GetCurrentAnimatorStateInfo(0).length);
        SceneManager.LoadScene(4);
        yield return null;
    }

    IEnumerator LowertoBossRoomTransition()
    {
        transitionanim.SetTrigger("End");
        yield return new WaitForSeconds(transitionanim.GetCurrentAnimatorStateInfo(0).length);
        SceneManager.LoadScene(5);
        yield return null;
    }

    IEnumerator LowertoAcademyTransition()
    {
        transitionanim.SetTrigger("End");
        yield return new WaitForSeconds(transitionanim.GetCurrentAnimatorStateInfo(0).length);
        SceneManager.LoadScene(6);
        yield return null;
    }
  
    IEnumerator BossRoomtoLowerTransition()
    {
        transitionanim.SetTrigger("End");
        yield return new WaitForSeconds(transitionanim.GetCurrentAnimatorStateInfo(0).length);
        SceneManager.LoadScene(4);
        yield return null;
    }
    IEnumerator BeneathDepthstoAcademyOutskirtsTransition()
    {
        transitionanim.SetTrigger("End");
        yield return new WaitForSeconds(transitionanim.GetCurrentAnimatorStateInfo(0).length);
        SceneManager.LoadScene(7);
        yield return null;
    }

}
 