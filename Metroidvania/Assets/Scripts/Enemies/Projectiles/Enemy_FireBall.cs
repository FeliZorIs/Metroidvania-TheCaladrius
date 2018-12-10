using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_FireBall : MonoBehaviour {

    public GameObject   player;
    public GameObject   SpriteObject;
    Animator            anim;

    public int          speed;
    float               angle;
    float               time;
    float               nextAction;

    Vector3             direction;
    Vector3             target;

    enum EnemyState
    {
        Spawning,
        Moving,
    }; EnemyState enemyState;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        SpriteObject = this.transform.GetChild(0).gameObject;
        anim = SpriteObject.GetComponent<Animator>();

        enemyState = EnemyState.Spawning;
        nextAction = 2f;

        target = player.transform.position;
        direction = (target - transform.position);

        angle = -1 * (Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (enemyState)
        { 
            case EnemyState.Spawning:
                target = player.transform.position;
                direction = (target - transform.position);

                angle = -1 * (Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg);
                break;

            case EnemyState.Moving:
                Debug.DrawLine(transform.position, direction);
                transform.Translate(direction * speed * Time.deltaTime);
                SpriteObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                break;
        }

        time += Time.deltaTime;
        if (time >= nextAction)
        {
            anim.SetTrigger("ATTACK");
            enemyState = EnemyState.Moving;
        }

    }

    //=============================
    //      Collider Stuff
    //=============================
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            player.GetComponent<Player>().health -= 1;
        }
    }
}
