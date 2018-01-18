using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PLAYER_CONTROL{
	Snap,
	Free,
	CursorFollow
}

public class PlayerControl : MonoBehaviour {
	public static PlayerControl Instance;

	public PLAYER_CONTROL controlType = PLAYER_CONTROL.Snap;
	public SceneMainManager sceneManager;
	public SpawnPointParent spawnPointParent;
	public GameObject bouncer;
	public GameObject inputButton;
	public Transform destinationL, destinationR;

	public float[] bouncerXPos;
	public float moveSpeed;
	public float lerpSpeed;
	public int currentBouncerIndex = 0;

	public bool flagLeft = false;
	public bool flagRight = false;

	public bool releaseLeft = false;
	public bool releaseRight = false;

	void Awake()
	{
		Instance = this;
	}

	public void Init()
	{
		bouncer.transform.localPosition = new Vector3(bouncerXPos[0],bouncer.transform.localPosition.y);
		currentBouncerIndex = 0;

		if(controlType == PLAYER_CONTROL.CursorFollow){
			inputButton.SetActive(false);
		}else{
			inputButton.SetActive(true);
		}	
	}

	public void ButtonLDown()
	{
		if(controlType == PLAYER_CONTROL.Snap){
			if(currentBouncerIndex > 0){
				bouncer.transform.localPosition = new Vector3(bouncerXPos[--currentBouncerIndex],bouncer.transform.localPosition.y);
			}
		}else if(controlType == PLAYER_CONTROL.Free){
			releaseLeft = false;
			releaseRight = false;
			tHold = 0f;
			if(!flagRight){
				flagLeft = true;
			}
		}
	}

	public void ButtonRDown()
	{
		if(controlType == PLAYER_CONTROL.Snap){
			if(currentBouncerIndex < bouncerXPos.Length-1){
				bouncer.transform.localPosition = new Vector3(bouncerXPos[++currentBouncerIndex],bouncer.transform.localPosition.y);
			}
		}else if(controlType == PLAYER_CONTROL.Free){
			releaseLeft = false;
			releaseRight = false;
			tHold = 0f;
			if(!flagLeft){
				flagRight = true;
			}
		}
	}

	public void ButtonLUp()
	{
		if(flagLeft){
			flagLeft = false;
//			releaseLeft = true;
			tRelease = moveSpeed;
		}


	}
	public void ButtonRUp()
	{
		if(flagRight){
			flagRight = false;
//			releaseRight = true;
			tRelease = moveSpeed;
		}
	}

	float tHold;
	float tRelease;
	void Update()
	{
//		if(sceneManager.spawning){
			if(controlType != PLAYER_CONTROL.CursorFollow){
				if(spawnPointParent.flagSpawning){
					if(flagLeft){
						//					if(bouncer.transform.localPosition.x <= bouncerXPos[0]){
						//						bouncer.transform.localPosition = new Vector3(bouncerXPos[0],bouncer.transform.localPosition.y);
						//						flagLeft = false;
						//
						//					}else{
						//						bouncer.transform.Translate(Vector3.left*moveSpeed);
						//					}
						if(bouncer.transform.localPosition.x <= bouncerXPos[0] || tHold >= 1f){
							bouncer.transform.localPosition = new Vector3(bouncerXPos[0],bouncer.transform.localPosition.y);
							flagLeft = false;
							tHold = 0;
						}else{
							tHold += Time.deltaTime * lerpSpeed;
							bouncer.transform.localPosition = Vector3.Lerp(bouncer.transform.localPosition,destinationL.localPosition,tHold);
						}


					}else if(flagRight){
						//					if(bouncer.transform.localPosition.x >= bouncerXPos[bouncerXPos.Length-1]){
						//						bouncer.transform.localPosition = new Vector3(bouncerXPos[bouncerXPos.Length-1],bouncer.transform.localPosition.y);
						//						flagRight = false;
						//					}
						//					else{
						//						bouncer.transform.Translate(Vector3.right*moveSpeed);
						//					}	

						if(bouncer.transform.localPosition.x >= bouncerXPos[bouncerXPos.Length-1] || tHold >= 1f){
							bouncer.transform.localPosition = new Vector3(bouncerXPos[0],bouncer.transform.localPosition.y);
							flagRight = false;
							tHold = 0;
						}else{
							tHold += Time.deltaTime * lerpSpeed;
							bouncer.transform.localPosition = Vector3.Lerp(bouncer.transform.localPosition,destinationR.localPosition,tHold);
						}
					}else if(releaseLeft){
						//					if(bouncer.transform.localPosition.x <= bouncerXPos[0]){
						//						bouncer.transform.localPosition = new Vector3(bouncerXPos[0],bouncer.transform.localPosition.y);
						//						releaseLeft = false;
						//					}else{
						//						if(tRelease <= 0){
						//							tRelease = 0;
						//							releaseLeft = false;
						//						}else{
						//							tRelease -= Time.deltaTime*2;
						//							bouncer.transform.Translate(Vector3.left*tRelease);
						//						}
						//
						//					}
					}else if(releaseRight){
						//					if(bouncer.transform.localPosition.x >= bouncerXPos[bouncerXPos.Length-1]){
						//						bouncer.transform.localPosition = new Vector3(bouncerXPos[bouncerXPos.Length-1],bouncer.transform.localPosition.y);
						//						releaseRight = false;
						//					}
						//					else{
						//						if(tRelease <= 0){
						//							tRelease = 0;
						//							releaseRight = false;
						//						}else{
						//							tRelease -= Time.deltaTime*2;
						//							bouncer.transform.Translate(Vector3.right*tRelease);
						//						}
						//					}	
					}
				}
				
			}else{
				if(Input.GetMouseButton(0)){
					Vector3 mousePosition = Input.mousePosition;
					mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

					if(mousePosition.x >= -5f && mousePosition.x <= 5f){
						bouncer.transform.position = new Vector3(mousePosition.x,bouncer.transform.position.y);
					}
				}	
			}
//		}
	}

	public void OnSelectControl(int value)
	{
		controlType = (PLAYER_CONTROL)value;
	}

	public Button buttonSnap, buttonFree, buttonCursorFollow;

	public void InitControlButton()
	{
		buttonSnap.interactable = controlType == PLAYER_CONTROL.Snap ? false : true;
		buttonFree.interactable = controlType == PLAYER_CONTROL.Free ? false : true;
		buttonCursorFollow.interactable = controlType == PLAYER_CONTROL.CursorFollow ? false : true;
	}

	public void OnSelectSnap()
	{
		controlType = PLAYER_CONTROL.Snap;
		buttonSnap.interactable = false;
		buttonFree.interactable = true;
		buttonCursorFollow.interactable = true;
	}

	public void OnSelectFree()
	{
		controlType = PLAYER_CONTROL.Free;
		buttonSnap.interactable = true;
		buttonFree.interactable = false;
		buttonCursorFollow.interactable = true;
	}

	public void OnSelectFollwoCursor()
	{
		controlType = PLAYER_CONTROL.CursorFollow;
		buttonSnap.interactable = true;
		buttonFree.interactable = true;
		buttonCursorFollow.interactable = false;
	}
}
