using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadTrigger : MonoBehaviour 
{
    public string LoadName;
    public string unloadName;

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (LoadName != null)              
                SceneLoader.Instance.Load(LoadName);

            if (unloadName != null)
                StartCoroutine(UnloadScene());
        }
    }

    IEnumerator UnloadScene()
    {
        yield return new WaitForSeconds(.10f);
        SceneLoader.Instance.UnLoad(unloadName);
    }
}
