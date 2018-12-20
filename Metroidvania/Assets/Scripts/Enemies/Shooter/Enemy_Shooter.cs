using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shooter : Enemy{
    
    public GameObject   player;
    public GameObject   bullet;
    float               waitTime;
    public bool         playerInRange;

    public float        healthMe;
    float               time;
    public float        nextAction;

    Animator            anim;
    SpriteRenderer      sr;

    public Material     originalMat;
    public Material     whiteMat;

    //particle effects
    public Transform    explode;
    public Transform    charging;

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

        time = 0;
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
                time += Time.deltaTime;
                if (time >= nextAction)
                {
                    StartCoroutine(Shooting());
                    time = 0;
                }
                break;
        }

        if (transform.GetChild(0).gameObject.GetComponent<Shooter_Collider>().PlayerInZone == true)
        {
            enemyState = EnemyState.Attacking;
        }
        else
        {
            enemyState = EnemyState.Idle;
        }

        if (health <= 0)
        {
            sr.material = originalMat;
            StartCoroutine(Death());
        }
    }


    //What to do when the player is in Range
    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Bullet_Player")
        {
            health -= 1;
        }
    }

  
    IEnumerator Shooting()
    {
        if (sr.flipX == false)
        {
            anim.SetBool("isAttacking", true);
            Instantiate(charging, this.transform.position + Vector3.right, Quaternion.identity);
            yield return new WaitForSeconds(1f);
            Instantiate(bullet, this.transform.position + Vector3.right, Quaternion.identity);
            anim.SetBool("isAttacking", false);

            yield return new WaitForSeconds(2f);
        }
        else
        {
            anim.SetBool("isAttacking", true);
            Instantiate(charging, this.transform.position + Vector3.left, Quaternion.identity);
            yield return new WaitForSeconds(1f);
            Instantiate(bullet, this.transform.position + Vector3.left, Quaternion.identity);
            anim.SetBool("isAttacking", false);

            yield return new WaitForSeconds(2f);
        }
    }

    IEnumerator Death()
    {
        anim.SetTrigger("isDead");
        yield return new WaitForSeconds(.5f);
        Instantiate(explode, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    IEnumerator GotHit()
    {
        sr.material = whiteMat;
        yield return new WaitForSeconds(.1f);
        sr.material = originalMat;
    }

}
