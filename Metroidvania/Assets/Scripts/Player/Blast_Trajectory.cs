using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blast_Trajectory : MonoBehaviour {

    public Vector3 direction;
    public float speed;
    public GameObject bulletCrash;
    float timer;
    public float ttk;

	// Use this for initialization
	void Start () 
    {
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () 
    {
        Trajectory(direction);

        timer += Time.deltaTime;

        if (timer >= ttk)
            Destroy(this.gameObject);
	}

    public void Trajectory(Vector2 direction)
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Ground")
        {
            Instantiate(bulletCrash, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

        if (collider.gameObject.tag == "Enemy")
        {
            Instantiate(bulletCrash, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Box")
        {
            Instantiate(bulletCrash, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
