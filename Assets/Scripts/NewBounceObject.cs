using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BOUNCE_DIRECTION{
	Left = 0,
	Right,
	Up
}

public class NewBounceObject : MonoBehaviour {
	public delegate void Destroy(GameObject obj);
	public event Destroy OnDestroy;

	public Collider2D thisCollider;
	public Rigidbody2D thisRigidbody;

	public Vector2[] BounceVelocity;
	public int currentBounce = 0;

	string tagBouncer = "Bouncer";
	string tagFloor = "Floor";
	string tagFinish = "Finish";

	[Header("New Game Mode")]
	public bool flagIsCustomBounce = false;
	public int customBounceCounter = 0;
	public BOUNCE_DIRECTION bounceDirection = BOUNCE_DIRECTION.Right;

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
		if(flagIsCustomBounce){
			if(currentBounce == 0){ //left
				bounceDirection = Random.Range(0,2) == 0 ? BOUNCE_DIRECTION.Up : BOUNCE_DIRECTION.Right;
			}else if(currentBounce == 3){ //right
				bounceDirection = Random.Range(0,2) == 0 ? BOUNCE_DIRECTION.Up : BOUNCE_DIRECTION.Left;
			}else{ //middle
				int tempDir = Random.Range(0,3);
				bounceDirection = (BOUNCE_DIRECTION) tempDir;
			}
		}

		float x = BounceVelocity[currentBounce].x ;

		if(flagIsCustomBounce){
			if(bounceDirection == BOUNCE_DIRECTION.Left){
				x = BounceVelocity[currentBounce].x * -1f;
			}else if(bounceDirection == BOUNCE_DIRECTION.Up){
				x = 0f;
			}
		}

		thisRigidbody.velocity = new Vector2(x,BounceVelocity[currentBounce].y);

		if(flagIsCustomBounce){
			if(bounceDirection == BOUNCE_DIRECTION.Left && currentBounce >= 0){
				currentBounce--;
			}else if(bounceDirection == BOUNCE_DIRECTION.Right && currentBounce <= 3){
				currentBounce++;
			}
			customBounceCounter++;
			if(customBounceCounter >= 4 && flagIsCustomBounce){
				flagIsCustomBounce = false;
			}
		}else{
			currentBounce++;
		}

		if(currentBounce >= BounceVelocity.Length) currentBounce = BounceVelocity.Length-1;
	}
	void Finish()
	{
		if(OnDestroy != null) OnDestroy(gameObject);
		Destroy(gameObject);
	}
	void Fall()
	{
		if(OnDestroy != null) OnDestroy(gameObject);
		Destroy(gameObject);
	}
}