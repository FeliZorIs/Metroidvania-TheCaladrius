using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lycan_HealthUI : MonoBehaviour {

    public GameObject Boss1;
    public Slider displayPHealth;
    float p_health;
    float segments;
    public float maxHP;

    // Use this for initialization
    void Start()
    {
        maxHP = Boss1.gameObject.GetComponent<Lycanthrope>().healthMe;
    }

    // Update is called once per frame
    void Update()
    {
        updateHealth();
    }

    public void updateHealth()
    {
        segments = 1 / maxHP;
        p_health = Boss1.gameObject.GetComponent<Lycanthrope>().healthMe;
        displayPHealth.value = segments * p_health;

        if (p_health <= 0)
            transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);
        else
            transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(true);

    }
}
