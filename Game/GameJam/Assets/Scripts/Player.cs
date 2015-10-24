using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

   
    public Rigidbody2D rb;
    public Transform groundedCheckC;
    public Transform groundedCheckL;
    public Transform groundedCheckR;

    public LayerMask whatIsGroundLayer;


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

}
