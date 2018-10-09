using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyMove : MonoBehaviour {

    public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        this.transform.Translate(Vector2.left * speed * Time.deltaTime);
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().health -= 1;
        }
    }

}
