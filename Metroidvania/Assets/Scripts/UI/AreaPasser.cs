using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaPasser : MonoBehaviour {

    public string passToPlayer;

    //Passes the name of the Area to the player, who then passes that to the MarkerManager
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
            collider.GetComponent<Player>().areaPasser = passToPlayer;
    }
}
