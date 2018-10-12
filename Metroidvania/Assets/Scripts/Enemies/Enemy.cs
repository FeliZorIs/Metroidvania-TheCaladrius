using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        EnemyManager.Instance.RegisterEnemy(this);		
	}

    void Demolish()
    {
        EnemyManager.Instance.DeregisterEnemy(this);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Demolish();
            Destroy(this.gameObject);
        }
    }
}
