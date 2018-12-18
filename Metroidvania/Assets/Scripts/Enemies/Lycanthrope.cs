using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lycanthrope : MonoBehaviour {

    //things needed to function
    public GameObject           player;
    public Animator             anim;
    public Animation            animation;
    public SpriteRenderer       sr;

    //checks to see if the player was on the left or the right
    bool                        PlayerOnLeft;
    bool                        PlayerOnRight;

    //rock to toss at player
    public GameObject           rock;
    
    //timer to the coroutines and markov number to choose which coroutines/animations run
    public float                nextAction;
    float                       newNextAction;
    float                       timer = 0;
    int                         markovNum;

    //delay to throw rock or to charge at player 
    public float                rockDelay;
    public float                chargeDelay;

    //distance from the player
    public float                distanceFromPlayer;

    //movement speed for walking towards the player
    public float                speed;
    bool                        moving = false;
    bool                        charging = false;

    //is player in the boss zone?
    public bool                 PlayerInZone;

    enum BossState
    {
        Idle,
        Stage1,
        Stage2,
        Stage3,
        Death
    }; BossState bossState;

	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        bossState = BossState.Idle;

        //this will be turned true when passed from BossArea1.cs Script
        PlayerInZone = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        //Orient itself to look in the direction of the player
        var relativePoint = transform.InverseTransformPoint(player.transform.position);
        if (relativePoint.x < 0.0)      //Player is to the left
            sr.flipX = false;
        else if (relativePoint.x > 0.0) //Player is to the right
            sr.flipX = true;	

        //what is the distance from the player?
        distanceFromPlayer = Mathf.Abs(player.transform.position.x - this.transform.position.x);

        //various stages of the boss depending on its 
        switch (bossState)
        { 
            case BossState.Idle:
                if (PlayerInZone == true)
                    anim.SetTrigger("lycan_nextStage");
                    bossState = BossState.Stage1;
                break;

            case BossState.Stage1:
                newNextAction = nextAction;
                timer += Time.deltaTime;
                if (timer >= newNextAction)
                {
                    markovNum = Random.Range(0, 100);
                    if (markovNum <= 5)                     //DoNothing
                        StartCoroutine(DoNothing());

                    if (markovNum > 5 && markovNum <= 15)   //Charge
                        StartCoroutine(Charge());
   
                    if (markovNum > 15 && markovNum <= 40)  //ThrowRock
                        StartCoroutine(ThrowRock());

                    if (markovNum > 40 && markovNum <= 80)  //SwipePlayer
                        StartCoroutine(SwipePlayer());

                    if (markovNum > 80 && markovNum <= 100)   //MoveToPlayer
                        StartCoroutine(MoveToPlayer());

                    //Returns timer to 0
                    timer = 0;
                }
                break;

            case BossState.Stage2:
                newNextAction = nextAction;
                timer += Time.deltaTime;
                if (timer >= newNextAction)
                {
                    markovNum = Random.Range(0, 100);
                }
                break;

            case BossState.Stage3:
                newNextAction = nextAction;
                timer += Time.deltaTime;
                if (timer >= newNextAction)
                {
                    markovNum = Random.Range(0, 100);
                }
                break;

            case BossState.Death:
                StartCoroutine(Death());
                break;
        }
        
        //When the MoveToPlayer or Charge coroutine
        if (moving == true)
        {
            if (sr.flipX == false) //player is to the left
                transform.Translate(Vector2.left * speed * Time.deltaTime);
            else                   //player is to the right
                transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
            return; 
	}

//==============================================
//              Coroutines
//==============================================
    
    //Move towards player
    IEnumerator MoveToPlayer()
    {
        Debug.Log("MoveToPlayer");
        anim.SetBool("lycan_move", true);
        moving = true;
        /*if (distanceFromPlayer >= 5.5f)
        {
            if (sr.flipX == false) //player is to the left
                transform.Translate(Vector2.left * speed * Time.deltaTime);
            else                   //player is to the right
                transform.Translate(Vector2.right * speed * Time.deltaTime);            
        }
        else
            yield return new WaitForSeconds(1f);*/
        yield return new WaitForSeconds(1f);
        anim.SetBool("lycan_move", false);
        moving = false;
        if (distanceFromPlayer <= 5.5f)
            StartCoroutine(SwipePlayer());
    }

    //Attack at close range
    IEnumerator SwipePlayer()
    {
        Debug.Log("SwipePlayer");
        if (distanceFromPlayer <= 5.5f)
        {
            anim.SetTrigger("lycan_attack");
            yield return new WaitForSeconds(1f);
        }
        else
            StartCoroutine(MoveToPlayer());
    }

    //Throw rock
    IEnumerator ThrowRock()
    {
        Debug.Log("ThrowRock");
        anim.SetTrigger("lycan_throw");
        yield return new WaitForSeconds(1f);
        if (sr.flipX == false) //Player to the left
        { 
            Vector3 SpawnLeft = new Vector3(transform.position.x -3.0f, transform.position.y, transform.position.z);
            Instantiate(rock, SpawnLeft, Quaternion.identity);
        }
        else
        {
            rock.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
            Vector3 SpawnRight = new Vector3(transform.position.x + 3.0f, transform.position.y, transform.position.z);
            Instantiate(rock, SpawnRight, Quaternion.identity);
        }
    }

    //Charge into wall
    IEnumerator Charge()
    {
        Debug.Log("Charge");
        anim.SetBool("lycan_move", true);
        moving = true;
        /*if (sr.flipX == false) //player is to the left
            transform.Translate(Vector2.left * (speed*3) * Time.deltaTime);
        else                   //player is to the right
            transform.Translate(Vector2.right * (speed*3) * Time.deltaTime);    
        */
        yield return new WaitForSeconds(1f);
        anim.SetBool("lycan_move", false);
        moving = false;
    }


    //Do nothing
    IEnumerator DoNothing()
    {
        Debug.Log("DoNothing");
        yield return new WaitForSeconds(1f);
    }

    IEnumerator NextStage()
    {
        anim.SetTrigger("lycan_nextStage");
        yield return new WaitForSeconds(1f);
        //changes shader depending on the stage it will go into
        //changes bossState depending on the health 
    }

    //Dies
    IEnumerator Death()
    {
        anim.SetTrigger("lycan_death");
        yield return new WaitForSeconds(60f);
        Destroy(this.gameObject);
    }

//==============================================
//              Collision detection
//==============================================

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<Player>().health -= 1;
        }
    }
}
