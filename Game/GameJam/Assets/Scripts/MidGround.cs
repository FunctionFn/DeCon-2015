using UnityEngine;
using System.Collections;

public class MidGround : MonoBehaviour {

    public Rigidbody2D rb;

    public float speed;
    public float speedIncrement;

	// Use this for initialization
	void Start () {
	
	}
    void Update()
    {
        speed += speedIncrement;
        Move();
    }

    void Move()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

}
