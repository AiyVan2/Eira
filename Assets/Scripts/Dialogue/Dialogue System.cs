using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class DialogueSystem : MonoBehaviour
{
    private int currentDialogueIndex = 0;

    //UI BUTTONS
    public GameObject dialogueBox;
    public Text dialogueText;
    public Text speakerText;
    public Button nextButton;
    public Button closeButton;
    public Button interactButton;
    public GameObject beneathEntrance;
    public GameObject playerControlls;
    public GameObject playerattackButton;
    public GameObject playerrangedattackButton;
    public GameObject playerhealButton;
    public GameObject playerManaUI;
    public GameObject houseBarrier;
    public GameObject lumenBarrier;

    public GameObject Book;
    public GameObject Elder;

    private string currentText;
    private float textSpeed = 0.02f;

   
    // Start of the Game Dialouge
    void Start()
    {
        StartCoroutine(StartDialogue());
    }

    // Interact Button For Playing the Dialouge
    public void InteractButtonClicked()
    {
        dialogueBox.gameObject.SetActive(true);
        PlayNextDialogue();
        interactButton.gameObject.SetActive(false);
        playerControlls.SetActive(false);
       
    }

    //Close Dialouge Button
    public void CloseButtonClicked()
    {
        dialogueBox.SetActive(false);
        nextButton.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);
        playerControlls.gameObject.SetActive(true);
    }

    //Next Dialouge Button
    public void NextButtonClicked()
    {
        nextButton.gameObject.SetActive(false);
        PlayNextDialogue();
      
    }
    //Start Dialouge Coroutine
    IEnumerator StartDialogue()
    {
        yield return new WaitForSeconds(1f);
        dialogueBox.gameObject.SetActive(true);
        StartCoroutine(TypeText(dialogues[0].text));
        yield return new  WaitForSeconds(3f);
        closeButton.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(false);
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

        if (currentDialogueIndex == 0)
        {
            nextButton.gameObject.SetActive(false);
            closeButton.gameObject.SetActive(true);
            Book.SetActive(true);
        }
        if (currentDialogueIndex == 2)
        {
            nextButton.gameObject.SetActive(false);
            closeButton.gameObject.SetActive(true);
            Book.SetActive(false);
            Elder.SetActive(true);
            houseBarrier.SetActive(false);
        }
        if (currentDialogueIndex == 3)
        {
            beneathEntrance.SetActive(true);
        }
        if (currentDialogueIndex == 9)
        {
            nextButton.gameObject.SetActive(false);
            closeButton.gameObject.SetActive(true);
            playerattackButton.gameObject.SetActive(true);
            playerrangedattackButton.gameObject.SetActive(true);
            playerhealButton.gameObject.SetActive(true);
            playerManaUI.gameObject.SetActive (true);
            Elder.SetActive(false);
            lumenBarrier.SetActive(false);
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
    new Dialogue { text = "Mmm... What a strange dream... It felt so real. I should probably get up and see what to read today", speaker = "Eira" },
    new Dialogue { text = "Hmm, what's this? It looks ancient... How did it end up here?", speaker = "Eira" },
    new Dialogue { text = "This journal... It’s about the Luminaria Academy. Written by... my parents? But they never talked about this place. Why did they leave this behind?", speaker = "Eira" },
    new Dialogue { text = "Elder, I found this book in our house. It's about the Luminaria Academy, written by my parents. Why didn’t you ever tell me about this?", speaker = "Eira" },
    new Dialogue { text = "Eira, there are some truths that are too painful to share... But I see now that you are ready to hear them. Your parents were brilliant scholars, devoted to the study of both technology and ancient magic. They envisioned the Luminaria Academy as a place where these two could coexist, a beacon of knowledge for the world.", speaker = "Elder" },
    new Dialogue { text = "But something went wrong. The academy... it became a place of darkness, corrupted by the very magic it sought to control. Your parents... they left you in my care when you were just a baby, before they vanished into the shadows of that place.", speaker = "Elder" },
    new Dialogue { text = "Your parents were part of a grand endeavor to restore ancient knowledge, but the academy fell into darkness. It became a place of danger and corruption. They left you with me for your safety, away from the peril that claimed their work.", speaker = "Elder" },
    new Dialogue { text = "This rod was their final gift to you, a weapon they created to protect you. It harnesses the power of lightning—both a tool and a weapon. But, Eira... the path to the academy is dangerous. I urge you to reconsider.", speaker = "Eira" },
    new Dialogue { text = "I have to know the truth, Elder. I need to find out what happened to them and what has become of the academy.", speaker = "Elder" },
    new Dialogue { text = "Very well. The only path to the academy lies through a cave known as The Beneath. It is a treacherous journey, but it is the only way. May the light guide your path, Eira.", speaker = "Elder" },
};
   
}