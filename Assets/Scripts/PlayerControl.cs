using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {
	public GameObject bouncer;

	public float[] bouncerXPos;

	public bool flagSnap = true;

	public int currentBouncerIndex = 0;

	public void ButtonLDown()
	{
		if(flagSnap){
			if(currentBouncerIndex > 0){
				bouncer.transform.localPosition = new Vector3(bouncerXPos[--currentBouncerIndex],bouncer.transform.localPosition.y);
			}
		}
	}
	public void ButtonRDown()
	{
		if(flagSnap){
			if(currentBouncerIndex < bouncerXPos.Length-1){
				bouncer.transform.localPosition = new Vector3(bouncerXPos[++currentBouncerIndex],bouncer.transform.localPosition.y);
			}
		}
	}

	public void ButtonLUp()
	{
		
	}
	public void ButtonRUp()
	{
		
	}

}
