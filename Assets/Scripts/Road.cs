using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour {
	public RoadController parent;
	public float speed = 0.2f;

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "RoadSpawn") parent.SpawnRoad();
	}

	void Update()
	{
		if(transform.position.x >= -20f){
			transform.Translate(Vector3.left*speed);
		}else{
			Destroy(gameObject);
		}
	}
}
