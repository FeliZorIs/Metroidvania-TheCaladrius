using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class GameController : MonoBehaviour 
{
    public static GameController gameController;

    public float playerPositionX;
    public float playerPositionY;
    public float playerPositionZ;

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

    public void save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        PlayerData data = new PlayerData();
        data.playerPosX = playerPositionX;
        data.playerPosY = playerPositionY;
        data.playerPosZ = playerPositionZ;

        bf.Serialize(file, data);
        file.Close();
    }

    public void load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        { 
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);

            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            playerPositionX = data.playerPosX;
            playerPositionY = data.playerPosY;
            playerPositionZ = data.playerPosZ;
        }
    }

    public void Delete()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
            File.Delete(Application.persistentDataPath + "/playerInfo.dat");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

[Serializable]
class PlayerData
{
    public float playerPosX;
    public float playerPosY;
    public float playerPosZ;
}
