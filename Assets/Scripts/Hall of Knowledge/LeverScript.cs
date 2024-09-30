using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class LeverScript: MonoBehaviour
{
    public enum Levers { Lever1, Lever2, Lever3, Lever4, Lever5 }
    public Levers currentLever;


    public GameObject Door1;
    public GameObject Door2;
    public GameObject Door3;
    public GameObject Door4;
    public GameObject Door5;

    public GameObject lever1;
    public GameObject lever2;
    public GameObject lever3;
    public GameObject lever4;
    public GameObject lever5;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            switch (currentLever)
            {
                case Levers.Lever1:
                    flipButton_1();
                    break;
                case Levers.Lever2:
                    flipButton_2();
                    break;
                case Levers.Lever3:
                    flipButton_3();
                    break;
                case Levers.Lever4:
                    flipButton_4();
                    break;
                case Levers.Lever5:
                    flipButton_5();
                    break;
            }
        }
    }

    public void flipButton_1()
    {
        Door1.SetActive(false);
        lever1.SetActive(false);
    }
    public void flipButton_2()
    {
        Door2.SetActive(false);
        lever2.SetActive(false);
    }
    public void flipButton_3()
    {
        Door3.SetActive(false);
        lever3.SetActive(false);
    }
    public void flipButton_4()
    {
        Door4.SetActive(false);
        lever4.SetActive(false);
    }
    public void flipButton_5()
    {
        Door5.SetActive(false);
        lever5.SetActive(false);
    }
}

