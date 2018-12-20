using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Alive : MonoBehaviour {

    public bool Boss1IsAlive;
    public bool Boss2IsAlive;

    public GameObject Wall;

	void Start () 
    {
		//just for test need to make true for a bit
        Boss2IsAlive = true;
        Wall.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Boss2IsAlive == true)
        {
            Wall.SetActive(true);
        }
	}
}
