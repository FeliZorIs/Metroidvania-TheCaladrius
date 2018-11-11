using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Crate : MonoBehaviour {

    public Transform partSys;
    public GameObject healthPot;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Bullet")
        {
            Instantiate(partSys, this.transform.position, Quaternion.identity);
            Instantiate(healthPot, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);            
        }
    }
}
