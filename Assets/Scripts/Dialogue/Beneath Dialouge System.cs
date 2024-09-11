using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class BeneathDialogueSystem : MonoBehaviour
{
    public int currentDialogueIndex = 0;

    //UI BUTTONS
    public GameObject dialogueBox;
    public Text dialogueText;
    public Text speakerText;
    public Button nextButton;
    public Button closeButton;
    public Button interactButton;
  

    public GameObject playerControlls;
    public GameObject pauseButton;
    public GameObject beneathdepthsBarrier;
    public GameObject edgeoutpostBarrier;

    public GameObject outpostLeader;
    public GameObject beneathDepths;

    private string currentText;
    private float textSpeed = 0.02f;


    // Interact Button For Playing the Dialouge
    public void InteractButtonClicked()
    {
        dialogueBox.gameObject.SetActive(true);
        PlayNextDialogue();
        interactButton.gameObject.SetActive(false);
        playerControlls.SetActive(false);
        pauseButton.gameObject.SetActive(false);

    }

    //Close Dialouge Button
    public void CloseButtonClicked()
    {
        dialogueBox.SetActive(false);
        nextButton.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);
        playerControlls.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(true);
    }

    //Next Dialouge Button
    public void NextButtonClicked()
    {
        nextButton.gameObject.SetActive(false);
        PlayNextDialogue();

    }

    private void PlayNextDialogue()
    {
        currentDialogueIndex++;
        StartCoroutine(TypeText(dialogues[currentDialogueIndex].text));
    }

    //Dialouge Display System
    IEnumerator TypeText(string text)
    {
        speakerText.text = dialogues[currentDialogueIndex].speaker;
        currentText = dialogues[currentDialogueIndex].text;
        dialogueText.text = "";
        foreach (char letter in currentText)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
        nextButton.gameObject.SetActive(true);

        if (currentDialogueIndex == 1)
        {
            nextButton.gameObject.SetActive(false);
            closeButton.gameObject.SetActive(true);
            beneathdepthsBarrier.SetActive(false);
            beneathDepths.SetActive(false);
        }
        if (currentDialogueIndex == 8)
        {
            nextButton.gameObject.SetActive(false);
            closeButton.gameObject.SetActive(true);
            outpostLeader.SetActive(false);
            edgeoutpostBarrier.SetActive(false);
        }
    }

    [System.Serializable]
    public class Dialogue
    {
        public string text;
        public string speaker;
        public string interactText;

    }
    private Dialogue[] dialogues = new Dialogue[]
{
     new Dialogue { text = "Random", speaker = "Eira" },
    new Dialogue { text = "This must be the way to the academy... But how do I get through?.", speaker = "Eira" },
    new Dialogue { text = "My name is Eira. I’m trying to reach the academy, but the path is blocked. Can you help me?", speaker = "Eira" },
    new Dialogue { text = "Who are you, and what business do you have in these dangerous caves?", speaker = "Outpost Leader" },
    new Dialogue { text = "The academy... Few dare to speak of it, and fewer still seek to enter its cursed halls. The gate you're talking about is locked, and for good reason. Creatures from the academy have been pouring into these caves, threatening everyone here.", speaker = "Outpost Leader" },
    new Dialogue { text = "What can I do to help? If I can clear the way, will you open the gate for me?", speaker = "Eira" },
    new Dialogue { text = "We’re struggling to seal two open gates on the left side of this cave. The monsters have made it nearly impossible. If you can close those gates, I'll make sure the path to the academy is open for you.", speaker = "Outpost Leader" },
    new Dialogue { text = "I’ll do it. Just point me in the right direction.", speaker = "Eira" },
    new Dialogue { text = "Head through there. But be careful—those creatures are not like anything you've faced before.", speaker = "Outpost Leader" }
    };

}