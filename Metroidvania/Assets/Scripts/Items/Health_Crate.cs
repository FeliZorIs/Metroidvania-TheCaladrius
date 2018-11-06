using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Crate : MonoBehaviour {

    public Transform partSys;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            partSys.transform.GetComponent<ParticleSystem>().Play();
        }
    }
}
