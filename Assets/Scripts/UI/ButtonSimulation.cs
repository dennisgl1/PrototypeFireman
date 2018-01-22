using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSimulation : MonoBehaviour {
	public GameObject[] prefabNewBounceObjects;
	public Transform bounceObjectParent;
	public void ButtonInstantiateOnClick(int index)
	{
		Instantiate(prefabNewBounceObjects[index],bounceObjectParent);
	}
}
