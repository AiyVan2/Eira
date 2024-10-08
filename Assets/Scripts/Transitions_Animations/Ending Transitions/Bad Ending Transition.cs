using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BadEndingTransition : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.LoadScene(0);
    }
}
