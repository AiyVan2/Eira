using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject playerControlls;
    public GameObject pauseButton;
    public GameObject resumeButton;
    public GameObject mainmenuButton;
    public GameObject quitButton;



    public void showpauseButtons()
    {
        Time.timeScale = 0f;
        playerControlls.SetActive(false);
        pauseButton.SetActive(false);
        resumeButton.SetActive(true);
        mainmenuButton.SetActive(true);
        quitButton.SetActive(true);
    }
    public void resume()
    {
        Time.timeScale = 1f;
        playerControlls.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(true);
        resumeButton.SetActive(false);
        mainmenuButton.SetActive(false);
        quitButton.SetActive(false);
    }
    public void mainmenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    public void quit()
    {
        Application.Quit();
    }
}
