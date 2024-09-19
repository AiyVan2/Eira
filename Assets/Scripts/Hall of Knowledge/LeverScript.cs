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

    public GameObject flipButton;
    public GameObject flipButton2;
    public GameObject flipButton3;
    public GameObject flipButton4;
    public GameObject flipButton5;

    public GameObject lever1;
    public GameObject lever2;
    public GameObject lever3;
    public GameObject lever4;
    public GameObject lever5;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
          switch (currentLever)
            {
                case Levers.Lever1:
                    flipButton.SetActive(true);
                    break;
                    case Levers.Lever2:
                    flipButton2.SetActive(true);
                    break;
                    case Levers.Lever3:
                    flipButton3.SetActive(true);
                    break;
                    case Levers.Lever4:
                        flipButton4.SetActive(true);
                    break;
                    case Levers.Lever5:
                    flipButton5.SetActive(true);
                    break;
            }
        }
    }

    public void flipButton_1()
    {
        Door1.SetActive(false);
        flipButton.gameObject.SetActive(false);
        lever1.SetActive(false);
    }
    public void flipButton_2()
    {
        Door2.SetActive(false);
        flipButton2.gameObject.SetActive(false);
        lever2.SetActive(false);
    }
    public void flipButton_3()
    {
        Door3.SetActive(false);
        flipButton3.gameObject.SetActive(false);
        lever3.SetActive(false);
    }
    public void flipButton_4()
    {
        Door4.SetActive(false);
        flipButton4.gameObject.SetActive(false);
        lever4.SetActive(false);
    }
    public void flipButton_5()
    {
        Door5.SetActive(false);
        flipButton5.gameObject.SetActive(false);
        lever5.SetActive(false);
    }
}

