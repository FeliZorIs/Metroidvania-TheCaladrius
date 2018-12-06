using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Summoner : MonoBehaviour {

    GameObject          player;
    public GameObject   Fireball;

    EnemyManager        EnemyManager;
    Enemy               grunt;
    Enemy               shooter;

    Vector2             position1;
    Vector2             position2;
    Vector2             position3;

    BoxCollider2D       detectionZone;
    CapsuleCollider2D   bodyZone;

    int                 markovNum;
    public bool         cr1;
    public bool         cr2;
    public bool         cr3;

    SpriteRenderer      sr;
    Animator            anim;

    enum EnemyState
    {
        Idle,
        CheckMarkov,
        SummonCreatures,
        BlastFire,
        Waiting,
    }; EnemyState enemyState;

    void Start()
    {
        detectionZone = GetComponent<BoxCollider2D>();
        bodyZone = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        EnemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();

        markovNum = Random.Range(0, 100);
        enemyState = EnemyState.Idle;
        cr1 = true;
        cr2 = true;
        cr3 = true;

        position1 = new Vector2(transform.position.x - 5, transform.position.y);
        position2 = new Vector2(transform.position.x -10, transform.position.y);
        position3 = new Vector2(transform.position.x -15, transform.position.y);

        grunt = EnemyManager.EnemiesInTotal[0];
        shooter = EnemyManager.EnemiesInTotal[1];
    }

	void Update () 
    {
        switch (enemyState)
        {
            case EnemyState.Idle:
                break;

            case EnemyState.CheckMarkov:
                cr1 = true;
                cr2 = true;
                cr3 = true;
                if (markovNum >= 70 && cr1 == true)
                    enemyState = EnemyState.SummonCreatures;
                else if (markovNum >= 50 && cr2 == true)
                    enemyState = EnemyState.BlastFire;
                else if (markovNum <= 49 && cr3 == true)
                    enemyState = EnemyState.Waiting;
                    break;

            case EnemyState.SummonCreatures:
                StartCoroutine(summon());
                //enemyState = EnemyState.CheckMarkov;
                break;

            case EnemyState.BlastFire:
                StartCoroutine(BasicAttack());
                //enemyState = EnemyState.CheckMarkov;
                break;
            
            case EnemyState.Waiting:
                StartCoroutine(cooldown());
                //enemyState = EnemyState.CheckMarkov;
                break;
        }
        Debug.Log(markovNum);
	}

    IEnumerator summon() 
    {
        if (cr1 == true)
        {
            cr1 = false;
            yield return new WaitForSeconds(1f);
            anim.SetTrigger("isAttacking");
            yield return new WaitForSeconds(1f);
            anim.SetTrigger("isSlamming");

            Instantiate(grunt, position1, Quaternion.identity);
            Instantiate(grunt, position2, Quaternion.identity);
            Instantiate(grunt, position3, Quaternion.identity);
            StartCoroutine(cooldown());
        }
        else
            StartCoroutine(cooldown());
    }

    IEnumerator cooldown() 
    {
        if (cr3 == true)
        {
            cr3 = false;
            yield return new WaitForSeconds(2f);
            markovNum = Random.Range(0, 100);

            enemyState = EnemyState.CheckMarkov;
        }
        else
            enemyState = EnemyState.CheckMarkov;
    }

    IEnumerator BasicAttack()
    {
        if (cr2 == true)
        {
            cr2 = false;
            yield return new WaitForSeconds(1f);
            StartCoroutine(cooldown());
        }
        else
            StartCoroutine(cooldown());
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            enemyState = EnemyState.CheckMarkov;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            enemyState = EnemyState.Idle;
        }
    }
}

/*if(markovNum >= 70 && cr1 == true)
{
    cr1 = false;
    cr2 = true;
    cr3 = true;
   
    StartCoroutine(summon());
}
else if (markovNum >= 50 && cr2 == true)
{
    cr1 = true;
    cr2 = false;
    cr3 = true;

    StartCoroutine(BasicAttack());
}
else if (markovNum <= 49 && cr3 == true)
{
    cr1 = true;
    cr2 = true;
    cr3 = false;

    StartCoroutine(cooldown());
}*/