using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Enemy))]

public class Enemy_Grunt : Enemy {

    public float        speed;
    public float        waitTime;
    public float        distance;

    float               tempSpeed;

    Vector2             startPosition;
    float               distanceTraveled;
    Vector2             currentPos;

    Animator            anim;
    SpriteRenderer      sr;

    public int          healthMe;

    // Materials to switch to
    public Material     whiteMat;
    public Material     originalMat;

    enum EnemyState
    {
        MovingLeft,
        MovingRight,

        WaitingLeft,
        WaitingRight,
    };
    EnemyState enemyState;

	// Use this for initialization
	void Start () 
    {
        tempSpeed = speed;

        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        enemyState = EnemyState.MovingLeft;
        startPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
    {
        currentPos = this.transform.position;        
        distanceTraveled = Vector2.Distance(startPosition, currentPos);

        switch (enemyState)
        { 
            case EnemyState.MovingLeft:
                anim.SetBool("isMoving", true);
                sr.flipX = false;
                tempSpeed = speed;
                this.transform.Translate(Vector2.left * tempSpeed * Time.deltaTime);
                if (distanceTraveled >= distance)
                {
                    enemyState = EnemyState.WaitingLeft;
                }
                break;

            case EnemyState.MovingRight:
                anim.SetBool("isMoving", true);
                sr.flipX = true;
                tempSpeed = speed;
                this.transform.Translate(Vector2.right * tempSpeed * Time.deltaTime);
                if (distanceTraveled >= distance)
                {
                    enemyState = EnemyState.WaitingRight;
                }
                break;

            case EnemyState.WaitingLeft:
                StartCoroutine(holdingLeft());
                break;
            case EnemyState.WaitingRight:
                StartCoroutine(holdingRight());
                break;
        }
        if (healthMe <= 0)
        {
            sr.material = originalMat;
            StartCoroutine(Death());
        }
	}

//==================================================
//               Collision Detection
//==================================================

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")       //if i hit the player
        {
            collision.gameObject.GetComponent<Player>().health -= 1;
        }

        if (collision.gameObject.tag == "Bullet_Player")  //if I'M hit by the player attack
        {
            healthMe -= 1;
            Debug.Log("health: " + healthMe);
            StartCoroutine(GotHit());
        }
    }

//==================================================
//                   Coroutines
//==================================================

    IEnumerator holdingLeft() 
    {
        tempSpeed = 0;
        anim.SetBool("isMoving", false);

        yield return new WaitForSeconds(waitTime);
        startPosition = this.transform.position;
        enemyState = EnemyState.MovingRight;       
    }

    IEnumerator holdingRight()
    {
        tempSpeed = 0;
        anim.SetBool("isMoving", false);

        yield return new WaitForSeconds(waitTime);
        startPosition = this.transform.position;
        enemyState = EnemyState.MovingLeft;
    }

    IEnumerator GotHit()
    {
        sr.material = whiteMat;
        yield return new WaitForSeconds(.1f);
        sr.material = originalMat;
    }

    IEnumerator Death()
    {
        anim.SetTrigger("isDead");
        yield return new WaitForSeconds(.5f);
        Destroy(this.gameObject);
    }
}
