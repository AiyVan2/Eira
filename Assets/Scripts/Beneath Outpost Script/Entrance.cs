using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Entrance : MonoBehaviour
{

    public enum lever { lever1, lever2};
    public lever currentLever;
    public GameObject OutpostAnimOne;
    public GameObject OutpostAnimTwo;
    public GameObject OutpostDoorOne;
    public GameObject OutpostDoorTwo;
    public GameObject BeneathOne;

    public GameObject detectOne;
    public GameObject detectTwo;

    public Button PullLever1;
    public Button PullLever2;

    private Animator OutpostOne;
    private Animator OutpostTwo;

    private bool lever1Pulled = false;
    private bool lever2Pulled = false;
   

    private void Start()
    {
        OutpostOne = OutpostAnimOne.GetComponent<Animator>();
        OutpostTwo = OutpostAnimTwo.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            switch (currentLever)
            {
                case lever.lever1:
                    PullLever1.gameObject.SetActive(true);
                break;
                case lever.lever2:
                    PullLever2.gameObject.SetActive(true);  
                break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PullLever1.gameObject.SetActive(false);
            PullLever2.gameObject.SetActive(false);
        }
    }

    public void PullLever1Clicked()
    {
        lever1Pulled = true;
        OutpostDoorOne.SetActive(true);
        OutpostOne.SetBool("DoorOpen", false);
        Destroy(detectOne);
        CheckIfBothLeversPulled();
    }

    public void PullLever2Clicked()
    {
        lever2Pulled = true;
        OutpostDoorTwo.SetActive(true);
        OutpostTwo.SetBool("DoorOpen", false);
        Destroy (detectTwo);
        CheckIfBothLeversPulled();
    }

    private void CheckIfBothLeversPulled()
    {
        if (lever1Pulled && lever2Pulled)
        {
            FinishFirstMission();
        }
    }

    public void FinishFirstMission()
    {
        BeneathOne.SetActive(false);
    }
}