using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UmbraSolisBossTrigger : MonoBehaviour
{
    public GameObject umbrasolisWake;
    public GameObject barrier;
    public GameObject UmbraSolis;
    public Animator umbrasolisAnimator;
    public CameraSwap swap;

    private void Start()
    {
        umbrasolisAnimator.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(fight());
        }
    }

    IEnumerator fight()
    {

        barrier.SetActive(true);
        swap.swaptoboosRoom();
        yield return new WaitForSeconds(1f);
        umbrasolisAnimator.enabled=true;
        umbrasolisAnimator.SetTrigger("Wake");
        yield return new WaitForSeconds(1f);
        umbrasolisWake.gameObject.SetActive(false);
        UmbraSolis.SetActive(true);
        gameObject.SetActive(false);

    }
}
