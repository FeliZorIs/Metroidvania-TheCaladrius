using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shooter : MonoBehaviour{
    
    public GameObject   player;
    public GameObject   bullet;
    float               waitTime;
    public bool         playerInRange;

    Animator            anim;
    SpriteRenderer      sr;

    enum EnemyState
    {
        Idle,
        Attacking
    }; EnemyState enemyState;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        var relativePoint = transform.InverseTransformPoint(player.transform.position);
        if (relativePoint.x < 0.0)      //Player is to the left
            sr.flipX = true;
        else if (relativePoint.x > 0.0) //Player is to the right
            sr.flipX = false;

        switch (enemyState)
        { 
            case EnemyState.Idle:
                break;
            case EnemyState.Attacking:
                break;
        }
    }


    //What to do when the player is in Range
    void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.gameObject.tag == "Player")
        {
            enemyState = EnemyState.Attacking;
            StartCoroutine(Shooting());
        }
        else //just stand there if the player isnt within range
        {
            anim.SetBool("isAttacking", false);
            return;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            return;
        }
    }

  
    IEnumerator Shooting()
    {
        if (sr.flipX == false)
        {
            anim.SetBool("isAttacking", true);
            yield return new WaitForSeconds(1f);
            Instantiate(bullet, this.transform.position + Vector3.right, Quaternion.identity);
            anim.SetBool("isAttacking", false);

            yield return new WaitForSeconds(2f);
            StartCoroutine(Shooting());
        }
        else
        {
            anim.SetBool("isAttacking", true);
            yield return new WaitForSeconds(1f);
            Instantiate(bullet, this.transform.position + Vector3.left, Quaternion.identity);
            anim.SetBool("isAttacking", false);

            yield return new WaitForSeconds(2f);
            StartCoroutine(Shooting());
        }
    }     
}
