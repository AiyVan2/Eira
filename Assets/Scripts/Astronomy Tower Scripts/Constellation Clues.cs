using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstellationClues : MonoBehaviour
{
    public enum clue { ClueA, ClueB, ClueC, ClueD };
    public clue Clues;
    public Text clueText;
    public CanvasGroup canvasGroup; // Assign this in the Inspector

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            switch (Clues)
            {
                case clue.ClueA:
                    clueText.text = "Spring Into Action";
                    break;
                case clue.ClueB:
                    clueText.text = "Ram's Horn";
                    break;
                case clue.ClueC:
                    clueText.text = "Fiery Spirit";
                    break;
                case clue.ClueD:
                    clueText.text = "The Golden Fleece";
                    break;
            }

            // Fade in the text
            StartCoroutine(FadeInText());
        }
    }

    private IEnumerator FadeInText()
    {
        canvasGroup.alpha = 0;
        canvasGroup.gameObject.SetActive(true);

        float fadeTime = 1f; // Adjust this to your liking
        float timer = 0;

        while (timer < fadeTime)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, timer / fadeTime);
            timer += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1;

        yield return new WaitForSeconds(3f);
        StartCoroutine(FadeOutText());
    }

    private IEnumerator FadeOutText()
    {
        float fadeTime = 1f; // Adjust this to your liking
        float timer = 0;

        while (timer < fadeTime)
        {
            canvasGroup.alpha = Mathf.Lerp(1, 0, timer / fadeTime);
            timer += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0;
        canvasGroup.gameObject.SetActive(false);
    }
}
