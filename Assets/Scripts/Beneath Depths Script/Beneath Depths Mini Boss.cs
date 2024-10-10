using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BeneathDepthsMiniBoss : MonoBehaviour
{
    public GameObject Doors;
    public GameObject Boss;
    public Animator collosalAnimator;
    public GameObject wakebossObject;
    public GameObject backgroundMusic;

    //Background Ambiance Music
    public GameObject backgroundambianceMusic;

    public CameraSwap swap;

    private void Start()
    {
       collosalAnimator.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            StartCoroutine(wakenaBoss());
        }
    }
    //public void bossDefeated()
    //{
    //    Doors.SetActive(false);
    //    swap.returntoPlayerCamera();
    //    Destroy(gameObject);
    //}

    IEnumerator wakenaBoss()
    {
       
        swap.swaptoboosRoom();
        yield return new WaitForSeconds(2f);
        collosalAnimator.enabled = true;
        collosalAnimator.SetTrigger("wake");
        yield return new WaitForSeconds(1f);
        backgroundambianceMusic.SetActive(false);
        backgroundMusic.SetActive(true);
        Doors.SetActive(true);
        Boss.SetActive(true);
        wakebossObject.SetActive(false);
        gameObject.SetActive(false);
    }
}