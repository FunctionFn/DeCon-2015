using UnityEngine;
using System.Collections;

public class DeadEnemy : MonoBehaviour {

    public Rigidbody2D rb;
    public float speed;
    public Vector2 launchSpeed;

    public int value;


	// Use this for initialization
	void Start () {
        rb.AddForce(launchSpeed);
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    void Move()
    {
        rb.AddForce(new Vector2(speed, 0));
    }

    public void SetValue(int val)
    {
        value = val;

    }

    public void Activate()
    {
        GameController.Inst.AddScore(value);

        Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);

    }
}
