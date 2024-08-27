using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BeneathDepthsMiniBoss : MonoBehaviour
{
    public GameObject Doors;
    public GameObject Boss;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Doors.SetActive(true);
            Boss.SetActive(true);
        }
    }

    private void Update()
    {
        if (Boss == null)
        {
            bossDefeated();
        }
    }
    private void bossDefeated()
    {
        Doors.SetActive(false);
        Destroy(gameObject);
    }
}