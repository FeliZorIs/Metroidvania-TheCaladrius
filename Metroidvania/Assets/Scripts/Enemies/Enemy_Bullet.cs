using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet : MonoBehaviour {

    public float        speed;
    public GameObject   player;

    Vector3             targetPos;
    Vector3             direction;

	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag("Player");
        targetPos = player.transform.position;

        direction = targetPos - transform.position;
	}
	
	// Update is called once per frame
	void Update () 
    {
        Debug.DrawRay(this.transform.position, direction);
        transform.Translate(direction * speed * Time.deltaTime);
	}
}
