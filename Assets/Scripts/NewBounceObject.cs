﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBounceObject : MonoBehaviour {
	public Collider2D thisCollider;
	public Rigidbody2D thisRigidbody;

	public Vector2[] BounceVelocity;
	public int totalBounce = 0;

	string tagBouncer = "Bouncer";
	string tagFloor = "Floor";
	string tagFinish = "Finish";

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == tagBouncer){
			Bounce();
		}else if(other.tag == tagFloor){
			Fall();
		}else if(other.tag == tagFinish){
			Finish();
		}
	}

	void Bounce()
	{
		thisRigidbody.velocity = BounceVelocity[totalBounce];
		totalBounce++;
		if(totalBounce >= BounceVelocity.Length) totalBounce = BounceVelocity.Length-1;

	}
	void Finish()
	{
		Destroy(gameObject);
	}
	void Fall()
	{
		Destroy(gameObject);
	}
}