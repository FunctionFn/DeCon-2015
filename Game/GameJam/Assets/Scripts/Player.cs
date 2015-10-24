using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

   
    public Rigidbody2D rb;

    public float speed;
    public float jumpHeight;

    float moveDirection;

	// Use this for initialization
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

        if(Input.GetButtonDown("Jump"))
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

    

}
