using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Pot : MonoBehaviour 
{
    public int giveHealth;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<Player>().health += giveHealth;
            Destroy(this.gameObject);
        }
    }
}
