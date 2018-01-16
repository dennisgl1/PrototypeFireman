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
	public float[] initialMoveThrust;
	public float moveBounceThrust;
	public float bounceThrust = 250f;
	
	int currentIndex;

	public void Init(int index)
	{
		currentIndex = index;
		thisRigidBody.AddRelativeForce(Vector2.right*initialMoveThrust[index]);
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
		}
	}

	void Bounce()
	{
		thisRigidBody.AddRelativeForce(new Vector2(moveBounceThrust,bounceThrust));
		
		if(OnBounce != null) OnBounce();
	}

	void Fall()
	{
		if(OnFall != null) OnFall(this);
	}
}
