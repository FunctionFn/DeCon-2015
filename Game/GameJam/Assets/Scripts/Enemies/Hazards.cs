﻿using UnityEngine;
using System.Collections;

public class Hazards : MonoBehaviour {
    public int damage;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void Activate()
    {
        Player.Inst.Damage(damage);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);

    }
}
