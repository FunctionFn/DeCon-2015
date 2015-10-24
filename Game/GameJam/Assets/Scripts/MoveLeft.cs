using UnityEngine;
using System.Collections;

public class MoveLeft : MonoBehaviour {
	public Rigidbody2D rb;
	public float SPEED;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
			rb.velocity=new Vector2(-SPEED, 0);
		
	}
}
