using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingChoices : MonoBehaviour
{
    public enum Choices {goodChoice, badChoice};
    public Choices choice;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            switch(choice)
            {
                case Choices.goodChoice:
                    SceneManager.LoadScene(14);
                    break;
                case Choices.badChoice:
                    SceneManager.LoadScene(15);
                    break;
            }
        }
    }
}
