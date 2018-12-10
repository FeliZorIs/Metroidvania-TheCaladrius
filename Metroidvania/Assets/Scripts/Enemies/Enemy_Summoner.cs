using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Summoner : Enemy {

    GameObject player;
    public GameObject Fireball;

    EnemyManager EnemyManager;
    Enemy grunt;
    Enemy shooter;

    Vector2 position1, position2, position3;
    Vector2 above1, above2, above3;

    BoxCollider2D detectionZone;
    CapsuleCollider2D bodyZone;

    int markovNum;
    float timer;
    public float nextTime;

    SpriteRenderer sr;
    Animator anim;

    enum EnemyState
    {
        Idle,
        Attacking,
        Dead,
    }; EnemyState enemyState;

    void Start()
    {        
        detectionZone = GetComponent<BoxCollider2D>();
        bodyZone = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        EnemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();

        markovNum = Random.Range(0, 1000);
        timer = 0;
        enemyState = EnemyState.Idle;

        position1 = new Vector2(transform.position.x - 5, transform.position.y);
        position2 = new Vector2(transform.position.x - 10, transform.position.y);
        position3 = new Vector2(transform.position.x - 15, transform.position.y);

        above1 = new Vector2(transform.position.x + .5f, transform.position.y + 1.5f);
        above2 = new Vector2(transform.position.x + 1.5f, transform.position.y + 2);
        above3 = new Vector2(transform.position.x + 2.5f, transform.position.y + 1.5f);

        grunt = EnemyManager.EnemiesInTotal[0];
        shooter = EnemyManager.EnemiesInTotal[1];
        
    }

    void FixedUpdate()
    {
        if (health <= 0)
        {
            enemyState = EnemyState.Dead;
        }     

        switch (enemyState)
        {
            case EnemyState.Idle:
                break;

            case EnemyState.Attacking:
                timer += Time.deltaTime;// time start
                if (timer >= nextTime) // if time is greater than next time, as in time over
                {
                    markovNum = Random.Range(0, 100);//get number
                    if (markovNum >= 85) //number is greater than chance
                    {
                        StartCoroutine(summon());//do thing
                        Debug.Log("Summon");
                    }

                    else if (markovNum >= 50)
                    {
                        StartCoroutine(BasicAttack());
                        Debug.Log("Blasting");
                    }

                    else
                    {
                        StartCoroutine(cooldown());
                        Debug.Log("Waiting");
                    }
                    timer = 0;// time resets
                }
                break;

            case EnemyState.Dead:
                bodyZone.enabled = false;
                detectionZone.enabled = false;

                timer += Time.deltaTime;
                if (timer >= (nextTime * .1))
                {
                    StartCoroutine(Died());
                    nextTime = 999;
                    timer = 0;
                }
                break;
        }
    }

//=====================================
//          Coroutines
//=====================================    
    IEnumerator summon()
    {
        yield return new WaitForSeconds(1f);
        anim.SetTrigger("isAttacking");
        yield return new WaitForSeconds(1f);
        anim.SetTrigger("isSlamming");

        Instantiate(grunt, position1, Quaternion.identity);
        Instantiate(grunt, position2, Quaternion.identity);
        Instantiate(grunt, position3, Quaternion.identity);
    }

    IEnumerator cooldown()
    {
        yield return new WaitForSeconds(1f);
    }

    IEnumerator BasicAttack()
    {
        yield return new WaitForSeconds(1f);
        anim.SetTrigger("isAttacking");
        yield return new WaitForSeconds(1f);
        anim.SetTrigger("isSlamming");

        Instantiate(Fireball, above1, Quaternion.identity);
        yield return new WaitForSeconds(.25f);
        Instantiate(Fireball, above2, Quaternion.identity);
        yield return new WaitForSeconds(.25f);
        Instantiate(Fireball, above3, Quaternion.identity);
    }

    IEnumerator Died()
    {
        anim.SetBool("isDead", true);
        yield return new WaitForSeconds(20); //seconds until deletion after death
        Destroy(this.gameObject);
    }


//=====================================
//              Collider Stuff
//=====================================
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("ENGAGE");
            enemyState = EnemyState.Attacking;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("Are you still there?");
            enemyState = EnemyState.Idle;
        }
    }
}
