using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health_UI : MonoBehaviour 
{
    public GameObject player;
    public Slider displayPHealth;
    float p_health;
    float segments;
    public float maxHP;

	// Use this for initialization
	void Start () 
    {
        maxHP = player.gameObject.GetComponent<Player>().health;  
	}
	
	// Update is called once per frame
	void Update () 
    {
        updateHealth();
	}

    public void updateHealth()
    {
        segments = 1 / maxHP;
        p_health = player.gameObject.GetComponent<Player>().health;
        displayPHealth.value = segments * p_health;

        if (p_health <= 0)       
            transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);
        else
            transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(true);

    }
}
