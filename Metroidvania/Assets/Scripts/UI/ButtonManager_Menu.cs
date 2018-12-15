using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager_Menu : MonoBehaviour 
{
    public static ButtonManager_Menu bmm;
    public static bool loaded = false;

    public void exitGame() 
    {
        Application.Quit();
    }

    public void newGame()
    {
        SceneManager.LoadScene("Area1");
        SceneManager.LoadScene("Player", LoadSceneMode.Additive);
    }

    public void loadGame()
    {
        loaded = true;
        SceneManager.UnloadScene(SceneManager.GetActiveScene());      
    }
}
