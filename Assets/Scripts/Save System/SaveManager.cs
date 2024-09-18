using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

[System.Serializable]
public class SceneData
{
    public string sceneName;
    public string[] gameObjectNames; // Add this line
    public bool[] gameObjectActiveStates;
    public Vector3[] gameObjectPositions;
    public Quaternion[] gameObjectRotations;
    // Add other properties you want to save, such as component values, etc.
}

public class SaveManager : MonoBehaviour
{
    private SceneData sceneData;

    public void SaveGame()
    {
        sceneData = new SceneData();
        sceneData.sceneName = SceneManager.GetActiveScene().name;

        // Get all game objects, including children
        List<GameObject> allGameObjects = new List<GameObject>();
        foreach (GameObject rootObj in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            GetAllChildGameObjects(rootObj, allGameObjects);
        }

        sceneData.gameObjectNames = new string[allGameObjects.Count];
        sceneData.gameObjectActiveStates = new bool[allGameObjects.Count];
        sceneData.gameObjectPositions = new Vector3[allGameObjects.Count];
        sceneData.gameObjectRotations = new Quaternion[allGameObjects.Count];

        for (int i = 0; i < allGameObjects.Count; i++)
        {
            GameObject obj = allGameObjects[i];
            sceneData.gameObjectNames[i] = obj.name;
            sceneData.gameObjectActiveStates[i] = obj.activeSelf;
            sceneData.gameObjectPositions[i] = obj.transform.position;
            sceneData.gameObjectRotations[i] = obj.transform.rotation;
        }

        string json = JsonUtility.ToJson(sceneData);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGame.dat");
        bf.Serialize(file, json);
        file.Close();
    }

    private void GetAllChildGameObjects(GameObject parent, List<GameObject> allGameObjects)
    {
        allGameObjects.Add(parent); // Add the parent itself
        foreach (Transform child in parent.transform)
        {
            GetAllChildGameObjects(child.gameObject, allGameObjects); // Recursively add child objects
        }
    }

    public void LoadGame()
    {
        ResetPlayerHealth();

        if (File.Exists(Application.persistentDataPath + "/savedGame.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGame.dat", FileMode.Open);
            string json = (string)bf.Deserialize(file);
            file.Close();

            sceneData = JsonUtility.FromJson<SceneData>(json);

            SceneManager.LoadScene(sceneData.sceneName);

            // Wait for the scene to finish loading
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        List<GameObject> allGameObjects = new List<GameObject>();
        foreach (GameObject rootObj in scene.GetRootGameObjects())
        {
            GetAllChildGameObjects(rootObj, allGameObjects);
        }

        for (int i = 0; i < sceneData.gameObjectNames.Length; i++)
        {
            GameObject obj = allGameObjects.Find(g => g.name == sceneData.gameObjectNames[i]);
            if (obj != null)
            {
                obj.SetActive(sceneData.gameObjectActiveStates[i]);
                obj.transform.position = sceneData.gameObjectPositions[i];
                obj.transform.rotation = sceneData.gameObjectRotations[i];
            }
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void ResetPlayerHealth()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.ResetHealth();
            }
        }
    }
}