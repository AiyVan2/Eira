using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainMenu_Play : MonoBehaviour
{

    public GameObject continueButton; // Assign this in the inspector to reference the "Continue" button

    private void Start()
    {
        // Check if the save file exists when the menu is loaded
        if (File.Exists(Application.persistentDataPath + "/savedGame.dat"))
        {
            continueButton.SetActive(true);  // Show the Continue button if save file exists
        }
        else
        {
            continueButton.SetActive(false); // Hide the Continue button if no save file exists
        }
    }

    public void NewGame()
    {
        if(File.Exists(Application.persistentDataPath + "/savedGame.dat"))
        {
            File.Delete(Application.persistentDataPath + "/savedGame.dat");
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void ContinueGame()
    {
        // Load the save game by going to the saved scene
        // This would be where you call your SaveManager's LoadGame method
        // Assuming you have a SaveManager in your scene
        FindObjectOfType<SaveManager>().LoadGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
