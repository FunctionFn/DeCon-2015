﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {


    public enum State : int { Base = 1, Stunned = 2, Swinging = 3, Jumping = 4, AirSwinging = 5}

    public State currentState;
    public Rigidbody2D rb;
    public Transform groundedCheckC;
    public Transform groundedCheckL;
    public Transform groundedCheckR;

    public LayerMask whatIsGroundLayer;


    public float speed;
    public float jumpHeight;
    public float damageMultiplier;
    public float damageHop;

    public float stunTime;
    public float invincibilityTime;

    protected float stunCountdownTimer;


    float moveDirection;

	// Use this for initialization

    private static Player _inst;
    public static Player Inst { get { return _inst; } }

    void Awake()
    {
        _inst = this;
    }

	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        moveDirection = Input.GetAxis("Horizontal");

        

        ControllerUpdate();
	}


    void ControllerUpdate()
    {
        Move();

        if(Input.GetButton("Jump") && groundedCheck())
        {
            Jump();
        }
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection * speed, rb.velocity.y);
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
    }

    bool groundedCheck()
    {
        if (Physics2D.OverlapPoint(new Vector2(groundedCheckC.transform.position.x, groundedCheckC.transform.position.y), whatIsGroundLayer) != null
            || Physics2D.OverlapPoint(new Vector2(groundedCheckL.transform.position.x, groundedCheckL.transform.position.y), whatIsGroundLayer) != null
            || Physics2D.OverlapPoint(new Vector2(groundedCheckR.transform.position.x, groundedCheckR.transform.position.y), whatIsGroundLayer) != null)
            return true;

        return false;
    }


    public void Damage(int damage, float stunMultiplier = 1)
    {
        rb.AddForce(new Vector2(-damage * damageMultiplier, damageHop));

        Stun(stunTime*stunMultiplier);
    }

    public void Stun(float time)
    {
        stunCountdownTimer = time;

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<BaseEnemy>())
        {
            other.GetComponent<BaseEnemy>().Activate();
        }
    }

}
