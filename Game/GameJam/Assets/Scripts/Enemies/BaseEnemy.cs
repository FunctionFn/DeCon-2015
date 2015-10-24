using UnityEngine;
using System.Collections;

public class BaseEnemy : MonoBehaviour {

    public enum Direction : int { Left = -1, Right = 1};

    public Rigidbody2D rb;

    public float speed;
    public Direction dir;

    public int damage;
    public float startingHealth;

    public float currentHealth;

	// Use this for initialization
	void Awake () {
	    
	}

    void Start()
    {
        currentHealth = startingHealth;
    }
	
	// Update is called once per frame
	public void Update () {
        MovementUpdate();

        if(currentHealth <= 0)
        {
            Destroy(this);
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
        
    }

}
