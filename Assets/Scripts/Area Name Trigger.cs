using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaNameTrigger : MonoBehaviour
{
    public enum Areas { LumenVillage, Beneath, BeneathEdgeOutpost, BeneathDepths, AcademyOutskirts, HallOfKnowledge, AstronomyTower, ForbiddenWing};
    public Areas area;

    public GameObject lumenvillageName;
    public GameObject beneathName;
    public GameObject beneathedgeoutpostName;
    public GameObject beneathdepthsName;
    public GameObject academyoutskirtsName;
    public GameObject hallofknowledgeName;
    public GameObject astronomytowerName;
    public GameObject forbiddenwingName;

    private Coroutine fadeCoroutine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }

            switch (area)
            {
                case Areas.LumenVillage:
                    fadeCoroutine = StartCoroutine(FadeInText(lumenvillageName.GetComponent<Text>(), 1f));
                    break;
                case Areas.Beneath:
                    fadeCoroutine = StartCoroutine(FadeInText(beneathName.GetComponent<Text>(), 1f));
                    break;
                case Areas.BeneathEdgeOutpost:
                    fadeCoroutine = StartCoroutine(FadeInText(beneathedgeoutpostName.GetComponent<Text>(), 1f));
                    break;
                case Areas.BeneathDepths:
                    fadeCoroutine = StartCoroutine(FadeInText(beneathdepthsName.GetComponent<Text>(), 1f));
                    break;
                case Areas.AcademyOutskirts:
                    fadeCoroutine = StartCoroutine(FadeInText(academyoutskirtsName.GetComponent<Text>(), 1f));
                    break;
                case Areas.HallOfKnowledge:
                    fadeCoroutine = StartCoroutine(FadeInText(hallofknowledgeName.GetComponent<Text>(), 1f));
                    break;
                case Areas.AstronomyTower:
                    fadeCoroutine = StartCoroutine(FadeInText(astronomytowerName.GetComponent<Text>(), 1f));
                    break;
                case Areas.ForbiddenWing:
                    fadeCoroutine = StartCoroutine(FadeInText(forbiddenwingName.GetComponent<Text>(), 1f));
                    break;  
            }
        }
    }

    private IEnumerator FadeInText(Text text, float duration)
    {
        text.gameObject.SetActive(true);
        Color originalColor = text.color;
        text.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / duration);
            text.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        text.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1);

        // Wait before fading out
        yield return new WaitForSeconds(2f);

        // Fade out
        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(1f - (elapsedTime / duration));
            text.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        text.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
        text.gameObject.SetActive(false);

        // Reset the coroutine reference
        fadeCoroutine = null;
    }
}