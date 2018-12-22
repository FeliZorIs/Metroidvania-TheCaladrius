using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PI_HealthUI : MonoBehaviour {

    public GameObject Boss2;
    public Slider displayPHealth;
    float p_health;
    float segments;
    public float maxHP;

    // Use this for initialization
    void Start()
    {
        maxHP = Boss2.gameObject.GetComponent<PowerIncontinence>().health;
    }

    // Update is called once per frame
    void Update()
    {
        updateHealth();
    }

    public void updateHealth()
    {
        segments = 1 / maxHP;
        p_health = Boss2.gameObject.GetComponent<PowerIncontinence>().health;
        displayPHealth.value = segments * p_health;

        if (p_health <= 0)
            transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);
        else
            transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(true);

    }
}
