using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_groundCheck : MonoBehaviour 
{
    public GameObject Player;
    public Animator anim;
    public Transform partSys;

	void Start () 
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        anim = Player.GetComponent<Animator>();
        partSys = Player.GetComponent<Player>().partSys;
	}
	
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Ground")
        {
            Instantiate(partSys, new Vector3(transform.position.x, transform.position.y - 1.8f, transform.position.z), Quaternion.identity);
            anim.SetTrigger("player_jump_land");
            Player.GetComponent<Player>().grounded = true;
            Player.GetComponent<Player>().landedPosition = Player.transform.position;
            Player.GetComponent<Player>().Jcount = 0;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Ground")
        {
            Player.GetComponent<Player>().grounded = false;
            Player.GetComponent<Player>().Jcount = 1;
        }
    }
}
