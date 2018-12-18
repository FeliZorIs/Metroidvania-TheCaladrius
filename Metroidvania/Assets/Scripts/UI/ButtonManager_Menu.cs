using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager_Menu : MonoBehaviour
{
    public static ButtonManager_Menu bmm;
    public static bool loaded = false;
    public static bool newGame2 = false;

    public void exitGame() 
    {
        Application.Quit();
    }

    public void newGame()
    {
        newGame2 = true;
        SceneManager.LoadScene("Area1");
    }

    public void loadGame()
    {
        loaded = true;
        SceneManager.UnloadScene(SceneManager.GetActiveScene());      
    }
}
