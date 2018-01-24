using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DIFFICULTY{
	Easy, Medium, Hard
}

[System.Serializable]
public struct ObjectItem{
	public float delay;
	public GameObject bounceObject;
}

[System.Serializable]
public struct ObjectPattern{
	public DIFFICULTY difficulty;
	public ObjectItem[] objectItems;
}

public class BounceObjectPatternController : MonoBehaviour {

	public Transform bounceObjectParent;
	public bool flagIsInstantiating = false;

	public Button[] buttonsToDisable;

	[Header("Patterns")]
	public ObjectPattern[] objectPatterns;

	public void ButtoninstantiatePatternOnClick(int index)
	{
		InstantiatePattern(objectPatterns[index]);
		foreach(Button b in buttonsToDisable) b.interactable = false;
	}

	public void InstantiatePattern(ObjectPattern pattern)
	{
		StartCoroutine(InstantiatingPattern(pattern));
	}

	public void EnableButtons()
	{
		foreach(Button b in buttonsToDisable) b.interactable = true;
	}

	IEnumerator InstantiatingPattern(ObjectPattern pattern)
	{
		flagIsInstantiating = true;
		for(int i = 0;i<pattern.objectItems.Length;i++){
			yield return new WaitForSeconds(pattern.objectItems[i].delay);
			Instantiate(pattern.objectItems[i].bounceObject,bounceObjectParent);
		}
	}
}