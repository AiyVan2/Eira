using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverCanvas;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
       
        gameOverCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
        if ( !player.activeSelf)
        {
                gameOverCanvas.gameObject.SetActive(true);
            }
        }

    public void Retry()
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1;
    }
    public void Won()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
