using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {
	public BounceObjectParent bounceObjectParent;
	public float xPosToSpawn = -5.25f;
	public float xPosToDestroy = -11f;
	public float speed = 0.8f;

	public bool hasSpawnBounceObject = false;

	void SpawnObject()
	{
		hasSpawnBounceObject = true;
		bounceObjectParent.InitBounceObject();
	}


	void Update()
	{
		if(transform.localPosition.x <= xPosToDestroy){
			Destroy(gameObject);
		}else if(transform.localPosition.x <= xPosToSpawn){
			if(!hasSpawnBounceObject){
				SpawnObject();
			}
			transform.Translate(Vector3.left*speed);
		}else{
			transform.Translate(Vector3.left*speed);
		}
	}
}
