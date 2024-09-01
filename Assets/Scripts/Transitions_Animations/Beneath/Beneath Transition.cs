using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BeneathTransition : MonoBehaviour
{
    public enum DoorType {BeneathToOutpost, OutpostToBeneath, BeneathToBeneathDepths }
    public DoorType doorType;
    //Areas
    public GameObject BeneathArea;
    public GameObject BeneathEdgeOutpostArea;
    public Animator animator;
    public GameObject globalLight;


    [SerializeField] public Animator transitionanim;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            switch (doorType)
            {
                case DoorType.BeneathToOutpost:
                    StartCoroutine(BeneathEdgeOutpostEntrance());
                    break;
                case DoorType.OutpostToBeneath:
                    StartCoroutine(BeneathEdgeOutpostEntrance());
                    break;
                case DoorType.BeneathToBeneathDepths:
                    StartCoroutine(BeneathBeneathDepths());
                    break;
            }
        }
    }

    IEnumerator BeneathBeneathDepths()
    {
        transitionanim.SetTrigger("End");
        yield return new WaitForSeconds(transitionanim.GetCurrentAnimatorStateInfo(0).length);
        SceneManager.LoadScene(3);
        yield return null;
    }
    IEnumerator BeneathEdgeOutpostEntrance()
    {
        transitionanim.SetTrigger("End");
        yield return new WaitForSeconds(transitionanim.GetCurrentAnimatorStateInfo(0).length);
        BeneathArea.SetActive(false);
        BeneathEdgeOutpostArea.SetActive(true);
        transitionanim.SetTrigger("Start");
        yield return null;
    }
    IEnumerator BeneathEdgeOutpostExit()
    {
        transitionanim.SetTrigger("End");
        yield return new WaitForSeconds(transitionanim.GetCurrentAnimatorStateInfo(0).length);
        BeneathArea.SetActive(true);
        BeneathEdgeOutpostArea.SetActive(false);
        transitionanim.SetTrigger("Start");
        yield return null;
    }

}
