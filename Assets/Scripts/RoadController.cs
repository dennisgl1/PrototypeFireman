using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour {
	public GameObject prefabRoad;

	public void SpawnRoad()
	{
		GameObject roadObj = Instantiate(prefabRoad,transform);
		Road road = roadObj.GetComponent<Road>();
		road.parent = this;
	}
}
