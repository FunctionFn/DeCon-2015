using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {


    public enum State : int { Base = 1, Stunned = 2, Swinging = 3, Jumping = 4, AirSwinging = 5}

    public State currentState;
    public Rigidbody2D rb;
    public Transform groundedCheckC;
    public Transform groundedCheckL;
    public Transform groundedCheckR;
    public AttackHitbox groundSwingHitbox;
    public AttackHitbox airSwingHitbox;

    public LayerMask whatIsGroundLayer;


    public float speed;
    public float jumpHeight;
    public float damageMultiplier;
    public float damageHop;

    public float stunTime;
    public float invincibilityTime;

    public float stunCountdownTimer;
    public float invincibilityCountdownTimer;

    public float groundSwingStartup;
    public float groundSwingActive;
    public float groundSwingCooldown;

    float groundSwingTotalTime;

    public float airSwingStartup;
    public float airSwingActive;
    public float airSwingCooldown;

    float airSwingTotalTime;

    public float groundSwingTimer;
    public float airSwingTimer;

    public bool bIsInvincible;

    public float damage;

    float moveDirection;

	// Use this for initialization

    private static Player _inst;
    public static Player Inst { get { return _inst; } }

    void Awake()
    {
        _inst = this;
    }

	void Start () {
        currentState = State.Base;

        bIsInvincible = false;

        groundSwingTotalTime = groundSwingStartup + groundSwingActive + groundSwingCooldown;
        airSwingTotalTime = airSwingStartup + airSwingActive + airSwingCooldown;

        groundSwingHitbox.enabled = false;
        airSwingHitbox.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        moveDirection = Input.GetAxis("Horizontal");


        TimerUpdate();

        ControllerUpdate();

        
	}
    void TimerUpdate()
    {
        stunCountdownTimer -= Time.deltaTime;
        invincibilityCountdownTimer -= Time.deltaTime;
        groundSwingTimer -= Time.deltaTime;
        airSwingTimer -= Time.deltaTime;

        if (stunCountdownTimer <= 0 && currentState == State.Stunned)
        {
            currentState = State.Base;
        }

        if(invincibilityCountdownTimer <= 0)
        {
            bIsInvincible = false;
        }



        if(groundSwingTimer < (groundSwingTotalTime - groundSwingStartup) && groundSwingTimer > groundSwingCooldown && !groundSwingHitbox.bIsEnabled)
        {
            groundSwingHitbox.Enable();
            Debug.Log("Active");
        }
        else if (groundSwingTimer <= 0 && currentState != State.Stunned && currentState != State.AirSwinging)
        {
            if (groundedCheck())
            {
                currentState = State.Base;
            }
        }
        else if (groundSwingTimer < groundSwingCooldown)
        {
            groundSwingHitbox.Disable();
        }



        if (airSwingTimer < (airSwingTotalTime - airSwingStartup) && airSwingTimer > airSwingCooldown && !airSwingHitbox.bIsEnabled)
        {
            airSwingHitbox.Enable();
        }
        else if (airSwingTimer <= 0 && currentState != State.Stunned && currentState != State.Swinging)
        {
            if (groundedCheck())
            {
                currentState = State.Base;
            }
            else
            {
                currentState = State.Jumping;
            }
        }
        else if (airSwingTimer < airSwingCooldown)
        {
            airSwingHitbox.Disable();
        }


    }

    void ControllerUpdate()
    {
        if(currentState != State.Stunned)
        {
            Move();
        }
        

        if (currentState == State.Jumping)
        {
            if (groundedCheck())
            {
                currentState = State.Base;
            }
        }


        if(Input.GetButton("Jump") && groundedCheck() && currentState != State.Stunned)
        {
            Jump();
        }

        if(Input.GetButton("Fire1") && currentState != State.Stunned && currentState != State.Swinging)
        {
            
            if(groundedCheck())
            {
                GroundedSwing();
            }
            else
            {
                AirSwing();
            }
            currentState = State.Swinging;
        }
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection * speed, rb.velocity.y);
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        currentState = State.Jumping;
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
        rb.velocity = new Vector2(0, 0);
        rb.AddForce(new Vector2(-damage * damageMultiplier, damageHop));

        Stun(stunTime*stunMultiplier);
    }

    public void Stun(float time)
    {
        stunCountdownTimer = time;
        invincibilityCountdownTimer = invincibilityTime;
     
        currentState = State.Stunned;
        bIsInvincible = true;

    }

    public void GroundedSwing()
    {
        groundSwingTimer = groundSwingTotalTime;

    }

    public void AirSwing()
    {
        airSwingTimer = airSwingTotalTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<BaseEnemy>() && !bIsInvincible)
        {
            other.GetComponent<BaseEnemy>().Activate();
        }
    }

}
