﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraForTest : MonoBehaviour {

    GameObject      camera;
    public float    speed;
    float           tempSpeed;
    float           doubleSpeed;
	// Use this for initialization
	void Start () 
    {
        camera = transform.GetChild(0).gameObject;
        tempSpeed = speed;
        doubleSpeed = speed * 2;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = doubleSpeed;
        }
        else
        {
            speed = tempSpeed;
        }

		if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

    	if(Input.GetKey(KeyCode.Q))
        {
            camera.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
		if(Input.GetKey(KeyCode.E))
        {
            camera.transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
	}
}
