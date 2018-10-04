using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    Rigidbody2D rb;
    public bool grounded;

    public float speed;
    public float jumpF;

    public int Jcount = 0;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    private float tempSpeed;
    private Vector2 direction;

    enum PlayerState
    {
        Idle,
        Moving,
        Jumping,
        Attacking,
    };
    PlayerState playerState;

	// Use this for initialization
	void Start () 
    {
        grounded = true;
        rb = GetComponent<Rigidbody2D>();    		
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
        }
	}

    public void Movement()
    {
        direction = Vector2.zero;
        tempSpeed = 0;
        playerState = PlayerState.Idle;

        if (Input.GetKey(KeyCode.A))
        {
            playerState = PlayerState.Moving;
            tempSpeed = speed;
            direction = Vector2.left;
            transform.Translate(direction * tempSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
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

        //Line 84-94 makes you fall quicker rather than same time up, same time down
        if (rb.velocity.y < 0) 
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } 

        if (Input.GetKeyDown(KeyCode.Space) && grounded == true)
        {
            Jcount = 0;
            playerState = PlayerState.Jumping;
            rb.velocity = Vector2.up * jumpF;
        }

        //attempt at double jump
        if (grounded == false)
        {
            if (Input.GetKeyDown(KeyCode.Space) && Jcount < 1)
            {
                Jcount++;
                rb.velocity = Vector2.up * jumpF;
            }
        } 
    }
    

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = false;
        }
    }
}
