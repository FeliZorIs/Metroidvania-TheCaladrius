using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArea2 : MonoBehaviour {

    //GameObject that will be interacted with
    public GameObject Boss2;
    public GameObject Wall1;
    public GameObject Wall2;

    void Start()
    {
        Boss2 = GameObject.Find("PowerIncontinence");
        Wall1.gameObject.SetActive(false);
        Wall2.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Boss2.GetComponent<PowerIncontinence>().PlayerInZone = true;
            Wall1.gameObject.SetActive(true);
            Wall2.gameObject.SetActive(true);
        }
    }
}
