using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager_Game : MonoBehaviour 
{
    public GameObject pauseMenu;
    public GameObject Map;
    GameObject blaster;

    public bool isPaused;
    public bool openMap;

    void Start()
    {
        blaster = GameObject.Find("/Player/BlasterNotReally");
        pauseMenu.SetActive(false);
        Map.SetActive(false);

        isPaused = false;
        openMap = false;
    }

    void Update()
    {
        //lines to pause the game
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            //if map is open, close map instead of pause game
            if(openMap == true)
            {
                openMap = false;
                closeMap();
            }
            
            //if not paused, pause game and open menu
            else if (isPaused == false)
            {
                isPaused = true;
                pauseGame();
            }
            
            // if paused and menu open. do this 
            else
            {
                if (openMap == false)
                {
                    isPaused = false;
                    resumeGame();
                }
                else
                {
                    isPaused = false;
                    resumeGame();
                    openMap = false;
                    closeMap();
                }
            }
        }

        //lines to bring up map
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //if game is paused, do nothing
            if (isPaused == true)
            {
                return;
            }

            else if (openMap == false)
            {
                //blaster.SetActive(false);
                Time.timeScale = 0;
                openMap = true;
                OpenMap();
            }
            else
            {
                //blaster.SetActive(true);
                Time.timeScale = 1;
                openMap = false;
                closeMap();
            }
        }
    }


    //open and close the map
    public void OpenMap()
    {
        Time.timeScale = 0;
        openMap = true;
        Map.SetActive(true);
    }
    public void closeMap()
    {
        Time.timeScale = 1;
        openMap = false;
        Map.SetActive(false);
    }

    //pause and unpause the game to bring up the pause menu
    public void pauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
        pauseMenu.SetActive(true);
    }
    public void resumeGame() 
    {
        Time.timeScale = 1;
        isPaused = false;
        pauseMenu.SetActive(false);
    }

    //leave the game and go to the main menu
    public void leaveGame()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
