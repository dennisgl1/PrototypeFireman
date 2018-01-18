using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceObject : MonoBehaviour {
	public delegate void BounceObjectEvent();
	public delegate void BounceObjectFall(BounceObject g);
	public event BounceObjectEvent OnBounce;
	public event BounceObjectFall OnFinish;
	public event BounceObjectFall OnFall;

	[Header("Attributes")]
	public Rigidbody2D thisRigidBody;
	
	int currentIndex;

	public Vector2 currentVelocity;

	public void Init(int index)
	{
		currentIndex = index;

		thisRigidBody.velocity = new Vector2(2f,0f);
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "Bouncer"){
			Bounce();
		}else if(other.gameObject.tag == "Floor"){
			Fall();
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Finish"){
			if(OnFinish != null) OnFinish(this);
			Destroy(gameObject);
		}else if(other.gameObject.tag == "Floor"){
			Fall();
		}
	}
	float[] ran = new float[]{6.5f,7f};
	void Bounce()
	{
		//thisRigidBody.AddRelativeForce(new Vector2( Random.Range(moveBounceThrust,moveBounceThrust*2),Random.Range(bounceThrustLow,bounceThrusHigh)));
		thisRigidBody.velocity = new Vector2(Random.Range(1f,1.75f),7f);
		if(OnBounce != null) OnBounce();
	}

	void Fall()
	{
		if(OnFall != null) OnFall(this);
	}

	void Update()
	{
		currentVelocity = thisRigidBody.velocity;
	}
}
