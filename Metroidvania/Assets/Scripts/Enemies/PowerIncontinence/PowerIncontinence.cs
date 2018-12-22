using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerIncontinence : MonoBehaviour {
    
    //Things needed to work
    GameObject              player;
    Animator                anim;
    SpriteRenderer          sr;

    //Objects to lob and fight the player
    public GameObject       fireBall;
    public Transform        partsys_ChargeUp;

    //these bools check to see if the player is to the right or left of the player
    bool                    playerLeft;
    bool                    playerRight;

    //markov model things
    float                   timer = 0;
    public float            nextTime;
    float                   newNextTime;
    float                   markovNum;

    //determines distance from player
    public float            distanceFromPlayer;

    //is the player in the boss zone?
    public bool             PlayerInZone;

    //speed if anything needs it... idk
    public float            speed;
    float                   tempSpeed;

    public int              health;
    public GameObject       stairs;
    public GameObject       wall;

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
	    //initialize things and stuff
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        bossState = BossState.Idle;
        tempSpeed = speed;
        PlayerInZone = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        //what is the distance from me to player
        distanceFromPlayer = Mathf.Abs(player.transform.position.x - this.transform.position.x);

        //when health drops to 0
        if (health <= 0)
        {
            wall.gameObject.SetActive(false);
            stairs.gameObject.SetActive(true);
        }

        switch (bossState)
        {
            case BossState.Idle:
                if (PlayerInZone == true)
                    bossState = BossState.Stage1;
                break;

            case BossState.Stage1:
                timer += Time.deltaTime;
                newNextTime = nextTime;
                if (timer >= newNextTime)
                { 
                    //Do markov stuff
                    timer = 0;
                }
                break;

            case BossState.Stage2:
                timer += Time.deltaTime;
                newNextTime = nextTime * .66f;
                if (timer >= newNextTime)
                { 
                    //Do markov stuff
                    timer = 0;
                }
                break;

            case BossState.Stage3:
                timer += Time.deltaTime;
                newNextTime = nextTime * .33f;
                if (timer >= newNextTime)
                { 
                    //Do markov stuff
                    timer = 0;
                }
                break;

            case BossState.Death:
                break;
        }


	}

//====================================================
//                  Coroutines
//====================================================

    IEnumerator DoNothing()     //Do Nothing
    {
        yield return new WaitForSeconds(1f);
    }

//====================================================
//              Collision Detection
//====================================================
    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<Player>().health -= 1;
        }

        if (collider.gameObject.tag == "Bullet_Player")
        {
            health -= 1;
        }
    }
}
