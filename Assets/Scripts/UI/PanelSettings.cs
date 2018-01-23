using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelSettings : MonoBehaviour {
	private enum AXIS{ X, Y }

	[Header("Custom Attributes")]
	public int totalObjects = 3;
	public int totalBounceInput = 4;

	[Header("Prefab Object")]
	public NewBounceObject[] bounceObjects;

	[Header("Default Value")]
	public Vector2[] bounceInputDefaultValueA;
	public Vector2[] bounceInputDefaultValueB;
	public Vector2[] bounceInputDefaultValueC;

	[Header("Input A")]
	public InputField[] bounceInputAX;
	public InputField[] bounceInputAY;

	[Header("Input B")]
	public InputField[] bounceInputBX;
	public InputField[] bounceInputBY;

	[Header("Input C")]
	public InputField[] bounceInputCX;
	public InputField[] bounceInputCY;

	string prefKey = "BounceInput";

	#region UI Button
	public void ButtonSaveOnClick(int index)
	{
		SaveInputData(index);
	}

	public void ButtonResetDefaultOnClick()
	{
		for(int i = 0;i<totalObjects;i++){
			Vector2[] tempDefaultValue = i == 0 ? bounceInputDefaultValueA : i == 1 ? bounceInputDefaultValueB : bounceInputDefaultValueC;

			for(int j = 0;j<totalBounceInput;j++){
				SetData(i,j,AXIS.X,tempDefaultValue[j].x);
				SetData(i,j,AXIS.Y,tempDefaultValue[j].y);
			}
		}
		LoadInputData();
	}
	#endregion

	#region init
	void Awake()
	{
		PlayerPrefs.DeleteAll();
		if(PlayerPrefs.GetInt("HasInit",0) == 1){
			LoadInputData();
			PlayerPrefs.SetInt("HasInit",1);
		}
	}

	void OnEnable()
	{
		LoadInputData();
	}

	void OnDisable()
	{
		for(int i = 0;i<totalObjects;i++) SaveInputData(i);
	}
	#endregion

	void SaveInputData(int index)
	{
		InputField[] tempInputFieldX = index == 0 ? bounceInputAX : index == 1 ? bounceInputBX : bounceInputCX;
		InputField[] tempInputFieldY = index == 0 ? bounceInputAY : index == 1 ? bounceInputBY : bounceInputCY;

		for(int i = 0;i<totalBounceInput;i++){
			string tempPrefKey = GetPrefKeyID(index,i,AXIS.X);
			PlayerPrefs.SetFloat(tempPrefKey,float.Parse(tempInputFieldX[i].text));
			bounceObjects[index].BounceVelocity[i].x = float.Parse(tempInputFieldX[i].text);

			tempPrefKey = GetPrefKeyID(index,i,AXIS.Y);
			PlayerPrefs.SetFloat(tempPrefKey,float.Parse(tempInputFieldY[i].text));
			bounceObjects[index].BounceVelocity[i].y = float.Parse(tempInputFieldY[i].text);
		}
	}

	void LoadInputData()
	{
		for(int i = 0;i<totalObjects;i++){
			InputField[] tempInputFieldX = i == 0 ? bounceInputAX : i == 1 ? bounceInputBX : bounceInputCX;
			InputField[] tempInputFieldY = i == 0 ? bounceInputAY : i == 1 ? bounceInputBY : bounceInputCY;
			Vector2[] tempDefaultValue = i == 0 ? bounceInputDefaultValueA : i == 1 ? bounceInputDefaultValueB : bounceInputDefaultValueC;
			NewBounceObject tempObject = bounceObjects[i];
			print(tempObject.gameObject.name);
			for(int j = 0;j<totalBounceInput;j++){
//				print("i = "+i+", j = "+j);
				string tempPrefKey = GetPrefKeyID(i,j,AXIS.X);
//				print(tempPrefKey);
				tempInputFieldX[j].text = PlayerPrefs.GetFloat(tempPrefKey,tempDefaultValue[j].x).ToString();
				tempObject.BounceVelocity[j].x = PlayerPrefs.GetFloat(tempPrefKey,tempDefaultValue[j].x);

				tempPrefKey = GetPrefKeyID(i,j,AXIS.Y);
//				print(tempPrefKey);
				tempInputFieldY[j].text = PlayerPrefs.GetFloat(tempPrefKey,tempDefaultValue[j].y).ToString();
				tempObject.BounceVelocity[j].y = PlayerPrefs.GetFloat(tempPrefKey,tempDefaultValue[j].y);
			}
		}
	}
		
	void SetData(int objIndex,int bounceInputIndex , AXIS axis, float value)
	{
		string tempPrefKey =  prefKey +"/"+ bounceObjects[objIndex].gameObject.name +"/"+ bounceInputIndex.ToString() +"/"+ axis.ToString();
		PlayerPrefs.SetFloat(tempPrefKey,value);
	}

	string GetPrefKeyID(int objIndex,int bounceInputIndex, AXIS axis){
		return prefKey +"/"+ bounceObjects[objIndex].gameObject.name +"/"+ bounceInputIndex.ToString() +"/"+ axis.ToString();
	}
}