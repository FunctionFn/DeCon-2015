using UnityEngine;
using System.Collections;

public class AttackHitbox : MonoBehaviour {

    public bool bIsEnabled;

	// Use this for initialization
	void Start () {
        bIsEnabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void Enable()
    {
        bIsEnabled = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void Disable()
    {
        bIsEnabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }


}
