using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blast_Trajectory : MonoBehaviour {

    public Vector3 direction;
    public float speed;
    public GameObject bulletCrash;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        Trajectory(direction);	
	}

    public void Trajectory(Vector2 direction)
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Wall")
        { 
            
        }
    }
}
