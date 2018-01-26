using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DIFFICULTY{
	Easy, Medium, Hard
}

public enum OBJECT_TYPE{
	A,B,C
}

[System.Serializable]
public struct ObjectItem{
	public float delay;
	public OBJECT_TYPE objectType;
}

[System.Serializable]
public struct ObjectPattern{
	public DIFFICULTY difficulty;
	public ObjectItem[] objectItems;
}

public class BounceObjectPatternController : MonoBehaviour {
	public Transform bounceObjectParent;
	public bool flagIsInstantiating = false;
	public bool flagContinuous = false;

	public GameObject[] PrefabBounceObjects;

	public Button[] buttonsToDisable;
	public Button buttonStartContinuousInstantiate;
	public Button buttonStopContinuousInstantiate;

	[Header("Patterns")]
	public ObjectPattern[] objectPatterns;

	public List<GameObject> currentActiveObjects;

	public void ButtoninstantiatePatternOnClick(int index)
	{
		InstantiatePattern(objectPatterns[index]);
		foreach(Button b in buttonsToDisable) b.interactable = false;
		buttonStartContinuousInstantiate.gameObject.SetActive(false);
	}

	public void ButtonStartContinuousInstantiateOnClick()
	{
		flagContinuous = true;
		foreach(Button b in buttonsToDisable) b.interactable = false;
		InstantiateRandomPattern();
		buttonStartContinuousInstantiate.gameObject.SetActive(false);
		buttonStopContinuousInstantiate.gameObject.SetActive(true);
	}

	public void ButtonStopContinuousInstantiateOnClick()
	{
		StopAllCoroutines();

		foreach(GameObject g in currentActiveObjects) Destroy(g);
		
		flagContinuous = false;
		flagIsInstantiating = false;

		EnableButtons();
		buttonStopContinuousInstantiate.gameObject.SetActive(false);
		buttonStartContinuousInstantiate.gameObject.SetActive(true);
		currentActiveObjects.Clear();
	}

	public void InstantiatePattern(ObjectPattern pattern)
	{
		StartCoroutine(InstantiatingPattern(pattern));
	}

	void InstantiateRandomPattern()
	{
		int rnd = Random.Range(0,objectPatterns.Length);
		StartCoroutine(InstantiatingPattern(objectPatterns[rnd]));
	}

	public void EnableButtons()
	{
		foreach(Button b in buttonsToDisable) b.interactable = true;
		buttonStartContinuousInstantiate.gameObject.SetActive(true);
	}

	void RemoveObject(GameObject obj)
	{
		currentActiveObjects.Remove(obj);
		if(flagIsInstantiating && currentActiveObjects.Count <= 0){
			flagIsInstantiating = false;

			if(flagContinuous){
				InstantiateRandomPattern();
			}else{
				EnableButtons();
			}
		}
	}

	IEnumerator InstantiatingPattern(ObjectPattern pattern)
	{
		flagIsInstantiating = true;
		for(int i = 0;i<pattern.objectItems.Length;i++){
			yield return new WaitForSeconds(pattern.objectItems[i].delay);
			GameObject tempObj = (GameObject) Instantiate(PrefabBounceObjects[(int)pattern.objectItems[i].objectType],bounceObjectParent);
			tempObj.GetComponent<NewBounceObject>().OnDestroy += RemoveObject;
			currentActiveObjects.Add(tempObj);
		}
	}
}