using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter_Collider : MonoBehaviour {

    public bool PlayerInZone = false;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
            PlayerInZone = true;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
            PlayerInZone = false;
    }
}
