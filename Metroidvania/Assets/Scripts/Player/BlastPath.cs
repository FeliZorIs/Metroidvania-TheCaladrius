using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastPath : MonoBehaviour {

    public GameObject bullet;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pressed LMB.");
            Instantiate(bullet, this.transform);
        }
	}
}
