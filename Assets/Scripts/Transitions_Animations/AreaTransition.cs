using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class AreaTransition : MonoBehaviour
{
    public enum DoorType { HouseToLumen, LumenToBeneath, BackToHouse, CaveToAcademy, Upstair, Downstair, BeneathToOutpost, OutpostToBeneath}
    public DoorType doorType;
    public GameObject HouseArea;
    public GameObject LumenArea;
    public GameObject BeneathArea;
    public GameObject BedroomArea;
    public GameObject BeneathEdgeOutpostArea;


    public Button gotoBeneathButton;
    public Button backtoHouseButton;
    public Button gotoUpstairButton;
    public Button gotoDownstairButton;
    public Animator animator;

    public GameObject globalLight;
   

    [SerializeField] public Animator transitionanim;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            switch (doorType)
            {
                case DoorType.LumenToBeneath:
                    gotoBeneathButton.gameObject.SetActive(true);
                    break;
                case DoorType.BackToHouse:
                    backtoHouseButton.gameObject.SetActive(true);
                    break;
                case DoorType.CaveToAcademy:
                    Cave_Academy();
                    break;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            switch (doorType)
            {
                case DoorType.LumenToBeneath:
                    gotoBeneathButton.gameObject.SetActive(false);
                    break;
                case DoorType.BackToHouse:
                    backtoHouseButton.gameObject.SetActive(false);
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            switch (doorType)
            {
                case DoorType.HouseToLumen:
                    House_Road();
                    break;
                case DoorType.Upstair:
                    gotoUpstairButton.gameObject.SetActive(true);
                    break;
                case DoorType.Downstair:
                    gotoDownstairButton.gameObject.SetActive(true);
                    break;
                case DoorType.BeneathToOutpost:
                    Beneath_EdgeOutpostEntrance();
                    break;
                case DoorType.OutpostToBeneath:
                    Beneath_EdgeOutpostExit();
                    break;
            }
        }  
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            switch (doorType)
            {
                //case DoorType.HouseToLumen:
                //    break;
                case DoorType.Upstair:
                    gotoUpstairButton.gameObject.SetActive(false);
                    break;
                case DoorType.Downstair:
                    gotoDownstairButton.gameObject.SetActive(false);
                    break;
            }
        }
    }



    public void House_Road()
    {
        StartCoroutine(HouseRoad());
    }

    public void Lumen_Cave()
    {
        StartCoroutine (LumenCave());
    }
    public void Back_House()
    {
        StartCoroutine(BackHouse());
    }
    public void Cave_Academy()
    {
        StartCoroutine(CaveAcademy());
    }

    public void Upstair_House()
    {
        StartCoroutine(UpstairHouse()); 
    }

    public void Beneath_EdgeOutpostEntrance()
    {
        StartCoroutine(BeneathEdgeOutpostEntrance());
    }
    public void Beneath_EdgeOutpostExit()
    {
        StartCoroutine(BeneathEdgeOutpostExit());
    }

    IEnumerator HouseRoad()
    {
        transitionanim.SetTrigger("End");
        yield return new WaitForSeconds(transitionanim.GetCurrentAnimatorStateInfo(0).length);
        HouseArea.SetActive(false);
        LumenArea.SetActive(true);
        transitionanim.SetTrigger("Start");
        yield return null;
    }

    IEnumerator CaveAcademy()
    {
      transitionanim.SetTrigger("End");
       yield return new WaitForSeconds(transitionanim.GetCurrentAnimatorStateInfo(0).length);
       SceneManager.LoadScene(2);
    }

    IEnumerator LumenCave()
    {
        transitionanim.SetTrigger("End");
        yield return new WaitForSeconds(transitionanim.GetCurrentAnimatorStateInfo(0).length);
        LumenArea.SetActive(false) ; 
        BeneathArea.SetActive(true);
        Light2D light2D = globalLight.GetComponent<Light2D>();
        light2D.intensity = 0.6f;
        transitionanim.SetTrigger("Start");
        yield return null;
    }

    IEnumerator BackHouse()
    {
        transitionanim.SetTrigger("End");
        yield return new WaitForSeconds(transitionanim.GetCurrentAnimatorStateInfo(0).length);
        HouseArea.SetActive(true);
        BedroomArea.SetActive(false);
        LumenArea.SetActive(false);
        transitionanim.SetTrigger("Start");
        yield return null;
    }

    IEnumerator UpstairHouse()
    {
        transitionanim.SetTrigger("End");
        yield return new WaitForSeconds(transitionanim.GetCurrentAnimatorStateInfo(0).length);
        HouseArea.SetActive(false);
        BedroomArea.SetActive(true);
        transitionanim.SetTrigger("Start");
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
