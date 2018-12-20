using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lycanthrope : MonoBehaviour {

    //things needed to function
    public GameObject           player;
    public Animator             anim;
    public SpriteRenderer       sr;

    //boss health
    public int                  healthMe;
    int                         maxHealth;

    //checks to see if the player was on the left or the right
    bool                        PlayerOnLeft;
    bool                        PlayerOnRight;

    //rock to toss at player
    public GameObject           rock;
    
    //timer to the coroutines and markov number to choose which coroutines/animations run
    public float                nextAction;
    float                       newNextAction;
    float                       timer = 0;
    float                       markovNum;

    //delay to throw rock or to charge at player 
    public float                rockDelay;
    public float                chargeDelay;

    //distance from the player
    public float                distanceFromPlayer;

    //movement speed for walking towards the player
    public float                speed;
    float                       tempSpeed;
    bool                        moving = false;
    bool                        charging = false;

    //shaders to change back and forth to when hit
    public Material             originalMat;
    public Material             whitescreenMat;

    //is player in the boss zone?
    public bool                 PlayerInZone;

    //Am i dead?
    public bool                 isDead;
    public GameObject           Wall1;
    public GameObject           Wall2;

    enum BossState
    {
        Idle,
        Stage1,
        Stage2,
        Stage3,
        Death,
        NextStage
    }; BossState bossState;

	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        bossState = BossState.Idle;
        tempSpeed = speed;
        maxHealth = healthMe;
        isDead = false;
        //this will be turned true when passed from BossArea1.cs Script
        PlayerInZone = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (healthMe <= 0)
        {
            isDead = true;
        }

        if (isDead == false)
        {
            Wall1.SetActive(true);
            Wall2.SetActive(true);
        }
        else
        {
            Wall1.SetActive(false);
            Wall2.SetActive(true);
        }

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
                    bossState = BossState.Stage1;
                break;

            case BossState.Stage1:
                //what happens when health gets lowered to certain thresholds
                if (healthMe <= maxHealth * .66)
                    bossState = BossState.NextStage;
                if (healthMe <= 0)
                    bossState = BossState.Death;

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

                    if (markovNum > 80 && markovNum <= 100) //MoveToPlayer
                        StartCoroutine(MoveToPlayer());

                    //Returns timer to 0
                    timer = 0;
                }
                break;

            case BossState.Stage2:
                //what happens when health gets lowered to certain thresholds
                if (healthMe <= maxHealth * .33)
                    bossState = BossState.NextStage;
                if (healthMe <= 0)
                    bossState = BossState.Death;

                newNextAction = nextAction * .66f;
                timer += Time.deltaTime;
                if (timer >= newNextAction)
                {
                    markovNum = Random.Range(0, 100);
                    if (markovNum <= 2.5f)                      //DoNothing
                        StartCoroutine(DoNothing());

                    if (markovNum > 2.5f && markovNum <= 22.5f) //Charge
                        StartCoroutine(Charge());

                    if (markovNum > 22.5 && markovNum <= 42.5f) //ThrowRock
                        StartCoroutine(ThrowRock());

                    if (markovNum > 42.5f && markovNum <= 85)   //SwipePlayer
                        StartCoroutine(SwipePlayer());

                    if (markovNum > 85 && markovNum <= 100)     //MoveToPlayer
                        StartCoroutine(MoveToPlayer());

                    //Returns timer to 0
                    timer = 0;
                }
                break;

            case BossState.Stage3:
                //what happens when health gets lowered to certain thresholds
                if (healthMe <= 0)
                    bossState = BossState.Death;

                newNextAction = nextAction * .33f;
                timer += Time.deltaTime;
                if (timer >= newNextAction)
                {
                    markovNum = Random.Range(0, 100);
                    if (markovNum <= -1)                    //DoNothing
                        StartCoroutine(DoNothing());

                    if (markovNum > 0 && markovNum <= 20)   //Charge
                        StartCoroutine(Charge());

                    if (markovNum > 20 && markovNum <= 45)  //ThrowRock
                        StartCoroutine(ThrowRock());

                    if (markovNum > 45 && markovNum <= 90)  //SwipePlayer
                        StartCoroutine(SwipePlayer());

                    if (markovNum > 90 && markovNum <= 100) //MoveToPlayer
                        StartCoroutine(MoveToPlayer());

                    //Returns timer to 0
                    timer = 0;
                }
                break;

            case BossState.Death:
                newNextAction = .5f;
                timer += Time.deltaTime;
                if (timer >= newNextAction)
                {
                    newNextAction = 9999;
                    GetComponent<BoxCollider2D>().enabled = false;
                    GetComponent<Rigidbody2D>().simulated = false;
          
                    
                    StartCoroutine(Death());
                    timer = 0;
                }
                break;

            case BossState.NextStage:
                newNextAction = .1f;
                timer += Time.deltaTime;
                if (timer >= newNextAction)
                {
                    StartCoroutine(NextStage());
                }
                break;
        }
        
        //When the MoveToPlayer or Charge coroutine
        if (moving == true)
        {
            if (sr.flipX == false) //player is to the left
                transform.Translate(Vector2.left * tempSpeed * Time.deltaTime);
            else                   //player is to the right
                transform.Translate(Vector2.right * tempSpeed * Time.deltaTime);
        }
        else
            return;


	}


    void checkhealth()
    {
        //what happens when health gets lowered to certain thresholds
        if (healthMe <= maxHealth * .66 || healthMe <= maxHealth * .33)
            bossState = BossState.NextStage;
        if (healthMe <= 0)
            bossState = BossState.Death;
    }

//==============================================
//              Coroutines
//==============================================
    
    //Move towards player
    IEnumerator MoveToPlayer()
    {
        Debug.Log("MoveToPlayer");
        anim.SetBool("lycan_move", true);

        if (distanceFromPlayer <= 5.5f)
        {
            StartCoroutine(SwipePlayer());
            StopCoroutine(MoveToPlayer());
        }

        moving = true;
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
        tempSpeed = speed * 3;
        yield return new WaitForSeconds(chargeDelay);
        moving = true;
        /*if (sr.flipX == false) //player is to the left
            transform.Translate(Vector2.left * (speed*3) * Time.deltaTime);
        else                   //player is to the right
            transform.Translate(Vector2.right * (speed*3) * Time.deltaTime);    
        */
        yield return new WaitForSeconds(1f);
        anim.SetBool("lycan_move", false);
        moving = false;
        tempSpeed = speed;
    }


    //Do nothing
    IEnumerator DoNothing()
    {
        Debug.Log("DoNothing");
        yield return new WaitForSeconds(1f);
    }

    //Dies
    IEnumerator Death()
    {
        anim.SetTrigger("lycan_death");
        yield return new WaitForSeconds(60f);
        Destroy(this.gameObject);
    }

    //getting hit
    IEnumerator GotHit()
    {
        sr.material = whitescreenMat;
        yield return new WaitForSeconds(.1f);
        sr.material = originalMat;
    }

    IEnumerator NextStage()
    {
        anim.SetTrigger("lycan_nextStage");
        yield return new WaitForSeconds(1f);

        if (healthMe <= maxHealth * .66)
            bossState = BossState.Stage2;

        if (healthMe <= maxHealth * .33)
            bossState = BossState.Stage3;
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

        if (collider.gameObject.tag == "Bullet_Player")
        {
            healthMe -= 1;
            StartCoroutine(GotHit());
        }
    }
}
