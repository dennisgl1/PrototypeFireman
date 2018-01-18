using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointParent : MonoBehaviour {
	public GameObject prefabSpawnPoint;
	public BounceObjectParent bounceObjectParent;
	public float initialSpawnInterval = 8f;

	public bool flagSpawning = false;

	int totalSpawn = 0;

	public float t = 0;
	float spawnInterval;

	public void Init()
	{
		totalSpawn = 0;
		spawnInterval = initialSpawnInterval;
		t = 0;
	}

	void Update()
	{
		if(flagSpawning){
			if(t <= 0){
				InstantiateSpawnPoint();

			}else{
				t -= Time.deltaTime;
			}
		}
	}

	void InstantiateSpawnPoint()
	{
		GameObject temp = (GameObject) Instantiate(prefabSpawnPoint,transform);
		SpawnPoint sp = temp.GetComponent<SpawnPoint>();
		sp.bounceObjectParent = bounceObjectParent;
		ValidateSpawnInterval();
	}

	void ValidateSpawnInterval()
	{
		if(spawnInterval >= 2.5f){
			if(totalSpawn % 5 == 0){
				spawnInterval -= Random.Range(0.5f,1f);
			}
		}

		t = spawnInterval;
	}
}