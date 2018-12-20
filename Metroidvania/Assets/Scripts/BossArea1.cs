using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArea1 : MonoBehaviour {

    //GameObject that will be interacted with
    public GameObject       Boss1;
    public GameObject       Wall1;
    public GameObject       Wall2;

	void Start () 
    {
        Boss1 = GameObject.Find("Lycanthropy");
        Wall1.gameObject.SetActive(false);
        Wall2.gameObject.SetActive(false);
  	}	

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Boss1.GetComponent<Lycanthrope>().PlayerInZone = true;
            Wall1.gameObject.SetActive(true);
            Wall2.gameObject.SetActive(true);
        }
    }
}
