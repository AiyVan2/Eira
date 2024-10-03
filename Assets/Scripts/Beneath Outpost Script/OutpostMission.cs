using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OutpostMission: MonoBehaviour
{

    public GameObject outpostLeader;
    public enum lever { lever1, lever2};
    public lever currentLever;
    public GameObject OutpostAnimOne;
    public GameObject OutpostAnimTwo;
    public GameObject BeneathOne;

    public GameObject detectOne;
    public GameObject detectTwo;

    public GameObject lever1Anim;
    public GameObject lever2Anim;
    private Animator lever1Animation;
    private Animator lever2Animation;

    private Animator OutpostOne;
    private Animator OutpostTwo;

    public bool lever1Pulled = false;
    public bool lever2Pulled = false;

    public Text missionText;

    public LeverManager leverManager;
    private void Start()
    {
        OutpostOne = OutpostAnimOne.GetComponent<Animator>();
        OutpostTwo = OutpostAnimTwo.GetComponent<Animator>();
        lever1Animation = lever1Anim.GetComponent<Animator>();
        lever2Animation = lever2Anim.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            switch (currentLever)
            {
                case lever.lever1:
                    PullLever1Clicked();
                break;
                case lever.lever2:
                    PullLever2Clicked(); 
                break;
            }
        }
    }

    public void PullLever1Clicked()
    {
        leverManager.pullLever1 = true;
        lever1Animation.SetBool("Flipped1", true);
        OutpostOne.SetBool("DoorOpen", false);
        Destroy(detectOne);
        CheckIfBothLeversPulled();
    }

    public void PullLever2Clicked()
    {
        leverManager.pullLever2 = true;
        lever2Animation.SetBool("Flipped2", true);
        OutpostTwo.SetBool("DoorOpen", false);
        Destroy (detectTwo);
        CheckIfBothLeversPulled();
    }

    private void CheckIfBothLeversPulled()
    {
        if (leverManager.pullLever1 && leverManager.pullLever2)
        {
            FinishFirstMission();
        }
    }

    public void FinishFirstMission()
    {
        BeneathOne.SetActive(false);
        missionText.text = "Objective: Return to the outpost leader to unlock the Beneath Depths entrance.";
        outpostLeader.SetActive(true);
    }
}