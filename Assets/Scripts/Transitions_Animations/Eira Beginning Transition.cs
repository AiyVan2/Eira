using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class EiraBeginningTransition : MonoBehaviour
{
    public enum DoorType { HouseToLumen, LumenToBeneath, BackToHouse, Upstair, Downstair}
    public DoorType doorType;
    public GameObject HouseArea;
    public GameObject LumenArea;
    public GameObject BedroomArea;


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
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            switch (doorType)
            {
                case DoorType.BackToHouse:
                    backtoHouseButton.gameObject.SetActive(false);
                    break;
                case DoorType.LumenToBeneath:
                    gotoBeneathButton.gameObject.SetActive(false);
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
                    House_Lumen();
                    break;
                case DoorType.Upstair:
                    gotoUpstairButton.gameObject.SetActive(true);
                    break;
                case DoorType.Downstair:
                    gotoDownstairButton.gameObject.SetActive(true);
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
                case DoorType.Upstair:
                    gotoUpstairButton.gameObject.SetActive(false);
                    break;
                case DoorType.Downstair:
                    gotoDownstairButton.gameObject.SetActive(false);
                    break;
            }
        }
    }



    public void House_Lumen()
    {
        StartCoroutine(HouseLumen());
    }

    public void Lumen_Beneath()
    {
        gotoBeneathButton.gameObject.SetActive(false);
        StartCoroutine (LumenBeneath());
    }
    public void Back_House()
    {
        backtoHouseButton.gameObject.SetActive(false);
        gotoDownstairButton.gameObject.SetActive(false);
        StartCoroutine(BackHouse());
    }
    public void Upstair_House()
    {
        gotoUpstairButton.gameObject.SetActive(false);
        StartCoroutine(UpstairHouse()); 
    }


    IEnumerator HouseLumen()
    {
        transitionanim.SetTrigger("End");
        yield return new WaitForSeconds(transitionanim.GetCurrentAnimatorStateInfo(0).length);
        HouseArea.SetActive(false);
        LumenArea.SetActive(true);
        transitionanim.SetTrigger("Start");
        yield return null;
    }

    IEnumerator LumenBeneath()
    {
        transitionanim.SetTrigger("End");
        yield return new WaitForSeconds(transitionanim.GetCurrentAnimatorStateInfo(0).length);
        SceneManager.LoadScene(3);
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
}
