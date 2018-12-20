using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour 
{
    public static GameController    gameController;
    public Scene                    currentScene;
    public int                      currentSceneIndex;
    public bool                     dataDeleted = true;

    public float                    playerPositionX;
    public float                    playerPositionY;
    public float                    playerPositionZ;
    public int                      SceneIndex;
    public float                      health;


	// Use this for initialization
	void Awake() 
    {
        if (gameController == null)
        {
            DontDestroyOnLoad(gameObject);
            gameController = this;
        }

        else if (gameController != this)
        {
            Time.timeScale = 1;
        }
	}

    void Update()
    {
        currentScene = SceneManager.GetSceneByBuildIndex(currentSceneIndex);
    }

    public void save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        dataDeleted = false;

        PlayerData data = new PlayerData();
        data.playerPosX = playerPositionX;
        data.playerPosY = playerPositionY;
        data.playerPosZ = playerPositionZ;
        data.sceneIndex = SceneIndex;
        data.health = health;

        bf.Serialize(file, data);
        file.Close();
        Debug.Log(SceneIndex);
    }

    public void load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        { 
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);

            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            if (!(SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(data.sceneIndex)))
            {
                SceneManager.UnloadScene(currentSceneIndex);
                SceneManager.LoadScene(data.sceneIndex, LoadSceneMode.Additive);
            }
            else
                return;


            if (!(SceneManager.GetSceneByName("Player").isLoaded))
                SceneManager.LoadScene("Player", LoadSceneMode.Additive);

            //SceneManager.UnloadScene("Main Menu");
            playerPositionX = data.playerPosX;
            playerPositionY = data.playerPosY;
            playerPositionZ = data.playerPosZ;
            health = data.health;
            SceneIndex = data.sceneIndex;
        }            
    }

    public void Delete()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            File.Delete(Application.persistentDataPath + "/playerInfo.dat");
            dataDeleted = true;
        }
    }
}

[Serializable]
class PlayerData
{
    public float playerPosX;
    public float playerPosY;
    public float playerPosZ;
    public int sceneIndex;
    public float health;
}
