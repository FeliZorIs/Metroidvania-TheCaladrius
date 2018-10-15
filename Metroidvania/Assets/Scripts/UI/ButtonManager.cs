using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour 
{

    public void exitGame() 
    {
        Application.Quit();
    }

    public void newGame()
    {
        SceneManager.LoadScene("Test Area");
    }
}
