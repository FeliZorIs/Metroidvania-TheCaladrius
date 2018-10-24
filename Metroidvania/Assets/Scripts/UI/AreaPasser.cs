using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaPasser : MonoBehaviour {

    public GameObject theManager;
    public string areaPasser;

    void Start()
    {
        theManager = GameObject.Find("Managers/MarkerManager");
    }
	// Use this for initialization
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
            theManager.GetComponent<MarkerManager>().areaName = areaPasser;
    }
}
