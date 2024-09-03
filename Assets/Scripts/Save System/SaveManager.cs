using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public class SceneData
{
    public string sceneName;
    public GameObject[] gameObjects;
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

        GameObject[] gameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        sceneData.gameObjects = gameObjects;
        sceneData.gameObjectActiveStates = new bool[gameObjects.Length];
        sceneData.gameObjectPositions = new Vector3[gameObjects.Length];
        sceneData.gameObjectRotations = new Quaternion[gameObjects.Length];

        for (int i = 0; i < gameObjects.Length; i++)
        {
            sceneData.gameObjectActiveStates[i] = gameObjects[i].activeSelf;
            sceneData.gameObjectPositions[i] = gameObjects[i].transform.position;
            sceneData.gameObjectRotations[i] = gameObjects[i].transform.rotation;
        }

        string json = JsonUtility.ToJson(sceneData);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGame.dat");
        bf.Serialize(file, json);
        file.Close();
    }

    public void LoadGame()
    {
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
            Time.timeScale = 1f;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Restore the state of the GameObjects
        GameObject[] gameObjects = scene.GetRootGameObjects();
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].SetActive(sceneData.gameObjectActiveStates[i]);
            gameObjects[i].transform.position = sceneData.gameObjectPositions[i];
            gameObjects[i].transform.rotation = sceneData.gameObjectRotations[i];
        }

        // Remove the event listener
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}