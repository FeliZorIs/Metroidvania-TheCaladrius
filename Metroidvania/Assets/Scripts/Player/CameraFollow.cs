using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform player;
    public bool inBossArea1;

	// Use this for initialization
	void Start () {
        inBossArea1 = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (inBossArea1 == true)
        {
            transform.position = new Vector3(341.5f, -85, this.transform.position.z);
        }
        else
            this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, this.transform.position.z);
	}
}
