using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSave : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.gameObject.CompareTag("Player")) 
        { 
            SaveScene(); 
        } 
    }
    public void SaveScene() { BinaryFormatter formatter = new BinaryFormatter(); FileStream file = File.Create(Application.persistentDataPath + "/scene.dat"); formatter.Serialize(file, SceneManager.GetActiveScene()); file.Close(); }


    public void LoadScene()
    {
        if (File.Exists(Application.persistentDataPath + "/scene.dat"))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/scene.dat", FileMode.Open);
            Scene scene = (Scene)formatter.Deserialize(file);
            file.Close();
            SceneManager.LoadScene(scene.name);
        }
        Time.timeScale = 1.0f;
    }
}