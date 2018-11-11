using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastPath : MonoBehaviour {

    public GameObject bm;
    SpriteRenderer sr;
    
    public GameObject bullet;
	void Start () 
    {
        sr = GetComponentInParent<SpriteRenderer>();
        bm = GameObject.Find("/Managers/ButtonManager");
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (bm.GetComponent<ButtonManager_Game>().isPaused == false)
            {
                if (sr.flipX == false)
                {
                    bullet.GetComponent<Blast_Trajectory>().direction = Vector2.right;
                    Instantiate(bullet, new Vector3(transform.position.x + .5f, transform.position.y - .2f, transform.position.z), Quaternion.identity);
                }
                else
                {
                    bullet.GetComponent<Blast_Trajectory>().direction = Vector2.left;
                    Instantiate(bullet, new Vector3(transform.position.x - .5f, transform.position.y - .2f, transform.position.z), Quaternion.identity);
                }
            }
            else 
            {
                return;
            }
        }
	}
}
