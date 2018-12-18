using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArea1 : MonoBehaviour {

    //GameObject that will be interacted with
    public GameObject       Boss1;

	void Start () 
    {
        Boss1 = GameObject.Find("Lycanthropy");
  	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Boss1.GetComponent<Lycanthrope>().PlayerInZone = true;
        }
    }
}
