using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public GameObject           MyCamera;
    Rigidbody2D                 rb;
    SpriteRenderer              sr;
    Animator                    anim;

    public bool                 grounded;

    public float                speed;
    public float                jumpF;

    public int                  Jcount = 0;
    public int                  MaxJump;
    public float                health;

    public float                fallMultiplier = 2.5f;
    public float                lowJumpMultiplier = 2f;

    private float               tempSpeed;
    private Vector2             direction;

    //knockback
    public float                knockDur;
    public float                knockbackPwr;
    public float                knockbackForce;

    //send this to ManagerMarker
    public string               areaPasser;
    public GameObject           MarkerManager;

    //Sends this to the save function when called. Retrieved by AreaPasser
    public int                  buildIndex;

    public Transform            partSys;
    public Transform            loadIn;
    public Vector3              landedPosition;

    //if loaded by main menu, this becomes true
    public bool                 loadedFromMainMenu = false;

    //timer for the attack
    float                       attackTime;
    bool                        startTimer = false;

    enum PlayerState
    {
        Idle,
        Moving,
        Jumping,
        Attacking,
        KnockBack,
        Died,
        Dead,
    };
    PlayerState playerState;

	// Use this for initialization
	void Start () 
    {
        grounded = true;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        MyCamera = transform.GetChild(0).gameObject;
        MyCamera.GetComponent<NegativeScreen>().enabled = false;
        attackTime = 0;
        Instantiate(loadIn, transform.position, Quaternion.identity);

/*
        if (ButtonManager_Menu.loaded != false)
            transform.position = new Vector3(GameController.gameController.playerPositionX,
                GameController.gameController.playerPositionY + 2,
                GameController.gameController.playerPositionZ);
        else
        {
            ButtonManager_Menu.newGame2 = false;
            transform.position = new Vector3(0, 2, 0);
        }*/
    }

	void FixedUpdate () 
    {
        //sends the name of the current area to the MarkerManager. Retrieved by AreaPasser
        MarkerManager.GetComponent<MarkerManager>().areaName = areaPasser;
        
        switch (playerState)
        { 
            case PlayerState.Idle:
                Movement();
                Jump();
                break;
            case PlayerState.Moving:
                Movement();
                Jump();
                break;
            case PlayerState.Jumping:
                Jump();
                break;
            case PlayerState.Attacking:
                break;
            case PlayerState.KnockBack:
                if(sr.flipX == false)
                    StartCoroutine(knockback(knockDur, knockbackPwr, transform.position, knockbackForce));
                if(sr.flipX == true)
                    StartCoroutine(knockback(knockDur, knockbackPwr, transform.position, -knockbackForce));
                anim.SetTrigger("player_knockback");
                playerState = PlayerState.Idle;
                break;
            case PlayerState.Died:
                anim.SetTrigger("player_dead");
                playerState = PlayerState.Dead;
                break;
            case PlayerState.Dead:
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<Rigidbody2D>().simulated = false;
                speed = 0;
                tempSpeed = 0;
                break;
        }

        //ATTACK
        if (Input.GetKeyDown(KeyCode.K) && attackTime == 0)
        {
            transform.GetChild(1).GetComponent<BlastPath>().fireblaster();
            anim.SetTrigger("player_fight");
            startTimer = true;
        }

        if (startTimer == true)
        {
            attackTime += Time.deltaTime;
            if (attackTime >= .25f)
            {
                attackTime = 0;
                startTimer = false;                
            }               
        }

        //things that happen when you die
        if (health <= 0)
        {
            playerState = PlayerState.Died;
            MyCamera.GetComponent<NegativeScreen>().enabled = true;
        }
	}

    //To be able to move... obviously
    public void Movement()
    {
        direction = Vector2.zero;
        tempSpeed = 0;
        playerState = PlayerState.Idle;
        anim.SetBool("player_run", false);

        if (Input.GetKey(KeyCode.A)) // move left
        {
            sr.flipX = true;
            anim.SetBool("player_run", true);
            playerState = PlayerState.Moving;
            tempSpeed = speed;
            direction = Vector2.left;
            transform.Translate(direction * tempSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D)) // move right
        {
            sr.flipX = false;
            anim.SetBool("player_run", true);
            playerState = PlayerState.Moving;
            tempSpeed = speed;
            direction = Vector2.right;
            transform.Translate(direction * tempSpeed * Time.deltaTime);
        }
    }

    //Jump
    public void Jump()
    {
        playerState = PlayerState.Idle;        

        //makes you fall quicker rather than same time up, same time down
        if (rb.velocity.y < 0)
        {
            anim.SetBool("player_jump_down", true);
            anim.SetBool("player_jump_up", false);
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else
            anim.SetBool("player_jump_down", false);

        //Jumping
        if (Input.GetKeyDown(KeyCode.Space) && grounded == true)
        {
            anim.SetBool("player_jump_up", true);
            playerState = PlayerState.Jumping;
            rb.velocity = Vector2.up * jumpF;
        }

        //double jump
        if (grounded == false)
        {
            if (Input.GetKeyDown(KeyCode.Space) && Jcount < MaxJump)
            {
                Jcount++;
                if (Jcount >= 3)
                    anim.SetTrigger("player_jump_double");
                rb.velocity = Vector2.up * jumpF;
            }
        }

    }
    
//==============================================
//                Collision Control
//============================================== 
    
    public void OnCollisionEnter2D(Collision2D collision)
    {

        //when hit by enemy, sends player to hit animation
        if(collision.gameObject.tag == "Enemy")
        {
            playerState = PlayerState.KnockBack;
        }

        //When you fall out of bounds
        if (collision.gameObject.tag == "Out Of Bounds")
        {
            transform.position = landedPosition;
            anim.SetTrigger("player_knockback");
            health -= 1;
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "BossArea1") //when i enter a boss area, changes the position of the camera;
        {
            GetComponentInChildren<CameraFollow>().inBossArea1 = true;
        }

        if (collider.gameObject.tag == "BossArea2")
        {
            GetComponentInChildren<CameraFollow>().inBossArea2 = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "BossArea1") //when i leave a boss area, returns the camera back to normal
        {
            GetComponentInChildren<CameraFollow>().inBossArea1 = false;
        }

        if (collider.gameObject.tag == "BossArea2")
        {
            GetComponentInChildren<CameraFollow>().inBossArea2 = false;
        }
    }

//================================================
//                  Coroutines
//================================================

    //knockback
    public IEnumerator knockback(float knockDur, float knockbackPwr, Vector3 knockbackDir, float force)
    {
        
        float timer = 0;
        while (knockDur > timer)
        {
            timer += Time.deltaTime;
            rb.AddForce(new Vector3(knockbackDir.x * -force, knockbackDir.y * knockbackPwr, transform.position.z));
        }      
        yield return 0;
    }
}
