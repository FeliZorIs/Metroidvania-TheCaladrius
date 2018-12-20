using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet : MonoBehaviour {

    public float        speed;
    public GameObject   player;

    Vector3             targetPos;
    Vector3             direction;
    public Transform    bulletCrash;

    float               angle;
    Quaternion          rotation;
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

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<Player>().health -= 1;
            Instantiate(bulletCrash, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

        if (collider.gameObject.tag == "Ground")
        {
            Instantiate(bulletCrash, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
