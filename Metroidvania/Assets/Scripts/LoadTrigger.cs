using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadTrigger : MonoBehaviour 
{
    public string LoadName;
    public string unloadName;

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (LoadName != null)
                SceneManagerTutorial.Instance.Load(LoadName);

            if (unloadName != null)
                StartCoroutine("UnloadScene");
        }
    }

    IEnumerator UnloadScene()
    {
        yield return new WaitForSeconds(.10f);
        SceneManagerTutorial.Instance.UnLoad(unloadName);
    }
}
