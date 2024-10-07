using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BeneathDepthsDeadExplorerDialogue: MonoBehaviour
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

    public GameObject playerDash;
    public GameObject playerdashUI;

    private string currentText;
    private float textSpeed = 0.02f;

    public GameObject deadnpcTrigger;

    private PlayerInventory playerInventory;

    private void Start()
    {
        playerInventory = GameObject.FindWithTag("Player").GetComponent<PlayerInventory>();
    }


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

        if (currentDialogueIndex == 3)
        {
            nextButton.gameObject.SetActive(false);
            closeButton.gameObject.SetActive(true);
            playerDash.gameObject.SetActive(true);
            playerdashUI.gameObject.SetActive(true);
            playerInventory.hasDash = true;
            playerInventory.hasbeneathDepthsKey = true;
            deadnpcTrigger.SetActive(false);
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
    new Dialogue { text = "What happened here? This must be an explorer... But why did they come to a place like this?", speaker = "Eira" },
    new Dialogue { text = "These boots look sturdy... They might be quite valuable, I have a feeling they could be the key to getting across whatever challenges lie ahead.", speaker = "Eira" },
    new Dialogue { text = "I didn't expect to find someone... But I can’t let their fate be in vain. I’ll honor their memory by using these to continue my journey", speaker = "Eira" }
    };
}
