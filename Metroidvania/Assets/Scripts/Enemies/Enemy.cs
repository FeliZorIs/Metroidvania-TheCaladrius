using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int health;
    public bool isDead = false;

	void Start ()       //Takes this enemy and puts them in a list inside the EnemyManager.cs
    {
        EnemyManager.Instance.RegisterEnemy(this);		
	}

    void Demolish()
    {
        EnemyManager.Instance.DeregisterEnemy(this);
    }

    void Update()       //This will determine things i have yet to decide... death is amongst them
    { 
    
    
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet_Player")
        {
            //health -= 1;
        }
    }
}
