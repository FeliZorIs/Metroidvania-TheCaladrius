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

    public void fireblaster() 
    { 
        if (bm.GetComponent<ButtonManager_Game>().isPaused == false && bm.GetComponent<ButtonManager_Game>().openMap == false)
        {
            if (sr.flipX == false)
            {
                bullet.GetComponent<Blast_Trajectory>().direction = Vector2.right;
                bullet.GetComponent<SpriteRenderer>().flipX = false;
                Instantiate(bullet, new Vector3(transform.position.x + .75f, transform.position.y - .2f, transform.position.z), Quaternion.identity);
            }
            else
            {
                bullet.GetComponent<Blast_Trajectory>().direction = Vector2.left;
                bullet.GetComponent<SpriteRenderer>().flipX = true;
                Instantiate(bullet, new Vector3(transform.position.x - .75f, transform.position.y - .2f, transform.position.z), Quaternion.identity);
            }
        }
        else 
        {
            return;
        }        
    }
}
