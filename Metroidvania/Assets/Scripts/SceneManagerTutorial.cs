using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneManagerTutorial : MonoBehaviour 
{
    public static SceneManagerTutorial Instance { set; get; }

    private void Awake()
    {
        Instance = this;
        Load("Player");
        Load("TestScene1");
    }

    public void Load(string sceneName)
    {
        if (!SceneManager.GetSceneByName(sceneName).isLoaded)
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    public void UnLoad(string sceneName)
    {
        if (SceneManager.GetSceneByName(sceneName).isLoaded)
            SceneManager.UnloadScene(sceneName);
    }
}
