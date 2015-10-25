using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {


    public enum State : int { Base = 1, Stunned = 2, Swinging = 3, Jumping = 4, AirSwinging = 5}

    public State currentState;
    public Rigidbody2D rb;
    public Transform groundedCheckC;
    public Transform groundedCheckL;
    public Transform groundedCheckR;

    public Animator anim;

    public AnimationClip run;
    public AnimationClip jump;
    public AnimationClip fall;
    public AnimationClip stun;
    public AnimationClip swing;
    public AnimationClip airSwing;

    public AttackHitbox groundSwingHitbox;
    public AttackHitbox airSwingHitbox;

    public LayerMask whatIsGroundLayer;

    public AudioClip hitplayersound;
    public AudioClip hazardsound;

    public float speed;
    public float speedIncrement;
    public float jumpHeight;
    public float damageMultiplier;
    public float damageHop;

    public float stunTime;
    public float invincibilityTime;

    public float stunCountdownTimer;
    public float invincibilityCountdownTimer;

    public float groundSwingStartup;
    public float groundSwingActive;
    //public float initialGroundSwingCooldown;
    public float groundSwingCooldown;

    float groundSwingTotalTime;

    public float airSwingStartup;
    public float airSwingActive;
    //public float initialAirSwingCooldown;
    public float airSwingCooldown;

    float airSwingTotalTime;

    public float CoolDownReductionOnHit;

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
        anim.SetInteger("State", (int)currentState);
        anim = GetComponent<Animator>();

        bIsInvincible = false;

        //groundSwingCooldown = initialAirSwingCooldown;
        //airSwingCooldown = initialAirSwingCooldown;

        groundSwingTotalTime = groundSwingStartup + groundSwingActive + groundSwingCooldown;
        airSwingTotalTime = airSwingStartup + airSwingActive + airSwingCooldown;

        groundSwingHitbox.enabled = false;
        airSwingHitbox.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        moveDirection = Input.GetAxis("Horizontal");
        speed += speedIncrement;

        TimerUpdate();

        ControllerUpdate();

        //anim.SetInteger("State", (int)currentState);
        //anim.SetInteger("State", 0);
        
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
            //Debug.Log("Active");
        }
        else if (groundSwingTimer <= 0 && currentState != State.Stunned && currentState != State.AirSwinging)
        {
            groundSwingHitbox.Disable();
            //groundSwingCooldown = initialAirSwingCooldown;
            if (groundedCheck())
            {
                currentState = State.Base;
                anim.SetInteger("State", (int)currentState);
                
            }
        }
        else if (groundSwingTimer < groundSwingCooldown)
        {
            groundSwingHitbox.Disable();
            //groundSwingCooldown = initialAirSwingCooldown;
        }



        if (airSwingTimer < (airSwingTotalTime - airSwingStartup) && airSwingTimer > airSwingCooldown && !airSwingHitbox.bIsEnabled)
        {
            airSwingHitbox.Enable();
        }
        else if (airSwingTimer <= 0 && currentState != State.Stunned && currentState != State.Swinging)
        {
            //airSwingCooldown = initialAirSwingCooldown;
            airSwingHitbox.Disable();

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
            //airSwingCooldown = initialAirSwingCooldown;

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
                anim.SetInteger("State", (int)currentState);
            }
        }


        if(Input.GetButton("Jump") && groundedCheck() && currentState != State.Stunned)
        {
            Jump();
        }

        if(Input.GetButtonDown("Fire1") && currentState != State.Stunned && currentState != State.Swinging)
        {
            
            if(groundedCheck())
            {
                GroundedSwing();
                currentState = State.Swinging;
                anim.SetInteger("State", (int)currentState);
                
            }
            else
            {
                AirSwing();
                currentState = State.AirSwinging;
                anim.SetInteger("State", (int)currentState);
            }
            
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
        anim.SetInteger("State", (int)currentState);
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
        anim.SetInteger("State", (int)State.Stunned);
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

    public void ReduceCoolDown()
    {
        groundSwingTimer -= CoolDownReductionOnHit;
        airSwingTimer -= CoolDownReductionOnHit;

        //groundSwingCooldown = initialGroundSwingCooldown - CoolDownReductionOnHit;
        //airSwingCooldown = initialAirSwingCooldown - CoolDownReductionOnHit;
    }

    public void Kill()
    {
        Application.LoadLevel("level1");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<BaseEnemy>() && !bIsInvincible)
        {
            other.GetComponent<BaseEnemy>().Activate();
            GameController.Inst.ResetCombo();
            AudioSource.PlayClipAtPoint(hitplayersound, transform.position);
        }
        else if (other.GetComponent<Hazards>())
        {
            if(!other.GetComponent<MoveLeft>() || (other.GetComponent<MoveLeft>() && !bIsInvincible))
                other.GetComponent<Hazards>().Activate();
                AudioSource.PlayClipAtPoint(hazardsound, transform.position);
        }
        else if (other.GetComponent<DeathBox>())
        {
            Kill();
        }
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<DeadEnemy>() && !bIsInvincible)
        {
            other.gameObject.GetComponent<DeadEnemy>().Activate();
            GameController.Inst.AddCombo(1);
        }
    }

}
