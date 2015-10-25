using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {

    public Rigidbody2D rb;

    public float speed;
    public float speedIncrement;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        speed += speedIncrement;
        Move();
	}

    void Move()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

}
