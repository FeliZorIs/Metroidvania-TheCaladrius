using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSave : MonoBehaviour {

    void Awake()
    {
        if (ButtonManager_Menu.loaded == true)
        {
            GameController.gameController.load();
            transform.position = new Vector3
            (
                GameController.gameController.playerPositionX,
                GameController.gameController.playerPositionY,
                GameController.gameController.playerPositionZ
            );
            ButtonManager_Menu.loaded = false;
        }
    }

	void Update () 
    {
        if (Input.GetButtonDown("Save"))
        {
            GameController.gameController.playerPositionX = transform.position.x;
            GameController.gameController.playerPositionY = transform.position.y;
            GameController.gameController.playerPositionZ = transform.position.z;
            GameController.gameController.save();
        }

        if (Input.GetButtonDown("Load"))
        {
            GameController.gameController.load();
            transform.position = new Vector3
            (
                GameController.gameController.playerPositionX,
                GameController.gameController.playerPositionY,
                GameController.gameController.playerPositionZ
            );
        }
	}
}
