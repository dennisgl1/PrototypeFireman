using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour {
	public float speed = 0.2f;
	public bool flagMoving = true;

	void FixedUpdate()
	{
		if(flagMoving){
			if(transform.position.x >= -30f){
				transform.Translate(Vector3.left*speed);
			}else{
				transform.localPosition = new Vector3(30f,0,0);
			}
		}
	}
}