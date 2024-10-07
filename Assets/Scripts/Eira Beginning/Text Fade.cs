using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFade : MonoBehaviour
{

    public Text EiraClueText;
    private Coroutine fadeCoroutine;
    private bool isFadingOut = false; // To track if we're currently fading out

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (fadeCoroutine != null) // If any fade coroutine is running, stop it first
            {
                StopCoroutine(fadeCoroutine);
                fadeCoroutine = null;
            }

            if (!isFadingOut) // Start fade-in only if we're not in the middle of a fade-out
            {
                fadeCoroutine = StartCoroutine(FadeInText(EiraClueText, 1f));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (fadeCoroutine != null) // Stop any current fade coroutine
            {
                StopCoroutine(fadeCoroutine);
                fadeCoroutine = null;
            }

            isFadingOut = true; // Indicate that we are starting a fade-out
            fadeCoroutine = StartCoroutine(FadeOutText(EiraClueText, 1f));
        }
    }

    private IEnumerator FadeInText(Text text, float duration)
    {
        isFadingOut = false; // Ensure we're not in the process of fading out
        text.gameObject.SetActive(true);
        Color originalColor = text.color;
        text.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);

        // Fade in
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / duration);
            text.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        text.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1);
    }

    private IEnumerator FadeOutText(Text text, float duration)
    {
        Color originalColor = text.color;

        // Fade out
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(1f - (elapsedTime / duration));
            text.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        text.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
        text.gameObject.SetActive(false);

        // Reset state
        isFadingOut = false;
        fadeCoroutine = null;
    }
}