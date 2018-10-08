using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    public bool grounded;

    public float speed;
    public float jumpF;

    public int Jcount = 0;
    public float health;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    private float tempSpeed;
    private Vector2 direction;

    //knockback
    public float knockDur;
    public float knockbackPwr;
    public float knockbackForce;


    enum PlayerState
    {
        Idle,
        Moving,
        Jumping,
        Attacking,
        KnockBack,
    };
    PlayerState playerState;

	// Use this for initialization
	void Start () 
    {
        grounded = true;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
    {
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
                anim.SetBool("player_knockback", false);
                playerState = PlayerState.Idle;
                break;
        }
	}

    //To be able to move... obviously
    public void Movement()
    {
        direction = Vector2.zero;
        tempSpeed = 0;
        playerState = PlayerState.Idle;
        anim.SetBool("player_run", false);

        if (Input.GetKey(KeyCode.A))
        {
            sr.flipX = true;
            anim.SetBool("player_run", true);
            playerState = PlayerState.Moving;
            tempSpeed = speed;
            direction = Vector2.left;
            transform.Translate(direction * tempSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
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
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } 

        if (Input.GetKeyDown(KeyCode.Space) && grounded == true)
        {
            playerState = PlayerState.Jumping;
            rb.velocity = Vector2.up * jumpF;
        }

        //attempt at double jump
        if (grounded == false)
        {
            
            if (Input.GetKeyDown(KeyCode.Space) && Jcount < 2)
            {
                Jcount++;
                rb.velocity = Vector2.up * jumpF;
            }
        } 
    }
    
    //when you touch the ground, or hit an enemy
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
            Jcount = 0;
        }

        //when hit by enemy, sends player to hit animation
        if(collision.gameObject.tag == "Enemy")
        {
            anim.SetBool("player_knockback", true);
            playerState = PlayerState.KnockBack;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = false;
            Jcount = 1;
        }
    }

    //Coroutine for knockback
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
