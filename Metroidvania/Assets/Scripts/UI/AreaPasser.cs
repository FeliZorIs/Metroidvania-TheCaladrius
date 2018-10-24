using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaPasser : MonoBehaviour {

    public string passToPlayer;

    void Start()
    {
    }
	// Use this for initialization
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
            collider.GetComponent<Player>().areaPasser = passToPlayer;
    }
}
