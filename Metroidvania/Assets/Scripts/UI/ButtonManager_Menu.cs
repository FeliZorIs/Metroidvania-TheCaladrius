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
        SceneManager.LoadScene("Test Area");
    }

    public void loadGame()
    {
        loaded = true;
        SceneManager.LoadScene("Test Area");
    }
}
