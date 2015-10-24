using UnityEngine;
using System.Collections;

public class FlyingEnemy : BaseEnemy {

    public float frequency = 20.0f;
    public float MoveSpeed = 5.0f;
    public float magnitude = .5f;
    private float pos;
	// Use this for initialization
	void Start () {
        pos = transform.position.y;
	
	}
	
	// Update is called once per frame
	new void Update(){
        Movement();
        base.Update();
        
	}
    void Movement()
    {
        transform.position = new Vector2 (transform.position.x, pos + Mathf.Sin(Time.time * frequency) * magnitude);
    }
}
