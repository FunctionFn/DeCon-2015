﻿using UnityEngine;
using System.Collections;

public class DeadEnemy : MonoBehaviour {

    public Rigidbody2D rb;

    public Vector2 launchSpeed;

    public float value;


	// Use this for initialization
	void Start () {
        rb.AddForce(launchSpeed);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetValue(int val)
    {
        value = val;

    }
}
