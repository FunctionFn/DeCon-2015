﻿using UnityEngine;
using System.Collections;

public class BaseEnemy : MonoBehaviour {

    public enum Direction : int { Left = -1, Right = 1};

    public Rigidbody2D rb;
    public GameObject deadCoin;
    public AudioClip[] hitsounds;
    public float speed;
    public Direction dir;

    public int damage;
    public float startingHealth;

    public float knockedBackDistance;
    public float knockedBackHop;

    public float currentHealth;

    public float invincibilityTime;

    public bool bIsInvincible;
    public float invincibilityCountdownTimer;

    public int bounty;

	// Use this for initialization
	void Awake () {
        currentHealth = startingHealth;
        bIsInvincible = false;
	}

    void Start()
    {
        
    }
	
	// Update is called once per frame
	public void Update () {
        invincibilityCountdownTimer -= Time.deltaTime;


        if (invincibilityCountdownTimer <= 0)
        {
            bIsInvincible = false;
        }

        if(!bIsInvincible)
            MovementUpdate();

        if(currentHealth <= 0)
        {
            GameObject go = (GameObject)Instantiate(deadCoin, transform.position, Quaternion.identity);
            go.GetComponent<DeadEnemy>().SetValue(bounty);
            Destroy(this.gameObject);
        }
	}

    void MovementUpdate()
    {
        rb.velocity = new Vector2((int)dir * speed, rb.velocity.y);
    }

    public void Activate()
    {
        Player.Inst.Damage(damage);
    }

    public void OnHit()
    {
        currentHealth -= Player.Inst.damage;

        invincibilityCountdownTimer = invincibilityTime;
        bIsInvincible = true;
        AudioSource.PlayClipAtPoint(hitsounds[Random.Range(0, 3)], transform.position);
        rb.velocity = new Vector2(knockedBackDistance, knockedBackHop);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<AttackHitbox>() && other.GetComponent<AttackHitbox>().bIsEnabled && !bIsInvincible)
        {
            GameController.Inst.AddCombo(1);
            Player.Inst.ReduceCoolDown();
            OnHit();
        }
        else
        {
            
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);

    }
}
