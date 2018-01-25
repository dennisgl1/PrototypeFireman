using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSimulation : MonoBehaviour {
	public BounceObjectPatternController patternController;
	public GameObject[] prefabNewBounceObjects;
	public Transform bounceObjectParent;



	public void ButtonInstantiateOnClick(int index)
	{
		GameObject tempBounceObject = (GameObject)Instantiate(prefabNewBounceObjects[index],bounceObjectParent);
		//tempBounceObject.GetComponent<NewBounceObject>().OnDestroy += RemoveObject;
		//currentActiveObjects.Add(tempBounceObject);
	}

	void RemoveObject(GameObject obj)
	{
		//currentActiveObjects.Remove(obj);
//		if(patternController.flagIsInstantiating && currentActiveObjects.Count <= 0){
//			patternController.flagIsInstantiating = false;
//			patternController.EnableButtons();
//		}
	}
}