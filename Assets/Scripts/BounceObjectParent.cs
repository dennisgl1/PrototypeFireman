using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceObjectParent : MonoBehaviour {
	public HUD hud;
	public GameObject prefabBounceObject;
	//public List<BounceObject> bounceObjects;

	public void InitBounceObject()
	{
		GameObject tempObj = Instantiate(prefabBounceObject,transform);

		BounceObject bo = tempObj.GetComponent<BounceObject>();
		hud.RegisterBounceObjectEvent(bo);
		bo.OnFall += RemoveObject;
		bo.OnFinish += RemoveObject;
		//bounceObjects.Add(bo);
	}

	public void DestroyObjects()
	{
//		foreach(BounceObject g in bounceObjects) {
//			RemoveObject(g);
//		}
		foreach(Transform child in transform){
			RemoveObject(child.GetComponent<BounceObject>());
		}
	}

	public void RemoveObject(BounceObject g){
		hud.UnRegisterBounceObjectEvent(g);
		g.OnFall -= RemoveObject;
		//bounceObjects.Remove(g);
		Destroy(g.gameObject);
	}
}