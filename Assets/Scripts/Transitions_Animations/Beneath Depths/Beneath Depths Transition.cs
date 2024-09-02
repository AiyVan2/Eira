using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BeneathDepthsTransition : MonoBehaviour
{
    public enum Area { UppertoLower, LowertoAcademy, LowertoBoss, AcademytoLower, BosstoLower};
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
                case Area.AcademytoLower:
                    StartCoroutine(AcademytoLowerTransition());
                    break;
                case Area.BosstoLower:
                    StartCoroutine(BossRoomtoLowerTransition());
                    break;
            }
        }
    }

    IEnumerator UppertoLowerTransition()
    {
        transitionanim.SetTrigger("End");
        PlayerControlls.gameObject.SetActive(false);
        yield return new WaitForSeconds(transitionanim.GetCurrentAnimatorStateInfo(0).length);
        DepthsUpperLevel.SetActive(false);
        PlayerControlls.gameObject.SetActive(true);
        DepthsLowerLevel.SetActive(true);
        transitionanim.SetTrigger("Start");
        yield return null;
    }
    
    IEnumerator LowertoAcademyTransition()
    {
        transitionanim.SetTrigger("End");
        yield return new WaitForSeconds(0.2f);
        DepthsLowerLevel.SetActive(false);
        DepthsToAcademy.SetActive(true);
        transitionanim.SetTrigger("Start");
        yield return null;
    }
    IEnumerator LowertoBossRoomTransition()
    {
        transitionanim.SetTrigger("End");
        yield return new WaitForSeconds(transitionanim.GetCurrentAnimatorStateInfo(0).length);
        DepthsLowerLevel.SetActive(false);
        DepthsBossRoom.SetActive(true);
        transitionanim.SetTrigger("Start");
        yield return null;
    }
    IEnumerator AcademytoLowerTransition()
    {
        transitionanim.SetTrigger("End");
        yield return new WaitForSeconds(transitionanim.GetCurrentAnimatorStateInfo(0).length);
        DepthsToAcademy.SetActive(false);
        DepthsLowerLevel.SetActive(true);
        transitionanim.SetTrigger("Start");
        yield return null;
    }
    IEnumerator BossRoomtoLowerTransition()
    {
        transitionanim.SetTrigger("End");
        yield return new WaitForSeconds(transitionanim.GetCurrentAnimatorStateInfo(0).length);
        DepthsBossRoom.SetActive(false);
        DepthsLowerLevel.SetActive(true);
        transitionanim.SetTrigger("Start");
        yield return null;
    }

}
 