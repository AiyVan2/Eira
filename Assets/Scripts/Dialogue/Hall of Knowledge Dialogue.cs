using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HallofKnowledgeDialogue : MonoBehaviour
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

    private string currentText;
    private float textSpeed = 0.02f;

    //Archivus Spawn
    public GameObject archivustheLibrarian;


    public CameraSwap swap;
    public GameObject bossareaBarier;
    public GameObject thirdBook;


    IEnumerator slowarchivusSpanwn()
    {
        yield return new WaitForSeconds(2f);
        archivustheLibrarian.SetActive(true);
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
        bossareaBarier.SetActive(true);
        thirdBook.SetActive(false);
        swap.swaptoboosRoom();
        StartCoroutine(slowarchivusSpanwn());
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
    new Dialogue { text = "They sought power beyond knowledge and called it salvation. But the price was higher than any of us could bear. The academy fell, swallowed by the very shadows we sought to understand. We unleashed forces older than time, and now they linger in these halls", speaker = "Book" },
    new Dialogue { text = "Beware what lies beyond this page, for those who still guard this forbidden knowledge have become the monsters they once sought to control.", speaker = "Book" },
    new Dialogue { text = "Archivus is Coming... it is coming.....", speaker = "Book" }
    };
}
