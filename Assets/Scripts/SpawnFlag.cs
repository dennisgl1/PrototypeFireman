using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFlag : MonoBehaviour {
	public bool flag = false;

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "BounceObject"){
			flag = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == "BounceObject"){
			flag = false;
		}
	}
}
