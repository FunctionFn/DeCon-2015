using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour {
	public Rigidbody2D rb;
	public float SPEED;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.W))
		{
			rb.AddForce(new Vector2(0, SPEED));
		}
	}
}
