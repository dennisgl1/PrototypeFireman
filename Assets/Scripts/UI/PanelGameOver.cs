using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelGameOver : MonoBehaviour {
	public PlayerControl playerControl;
	public HUD hud;

	public Button buttonSnap;
	public Button buttonFree;
	public Button buttonCursorFollow;

	public Text textScore;

	public void Show()
	{
		InitControlButton();
		textScore.text = hud.GetScore().ToString();
		gameObject.SetActive(true);
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}

	void InitControlButton()
	{
		buttonSnap.interactable = playerControl.controlType == PLAYER_CONTROL.Snap ? false : true;
		buttonFree.interactable = playerControl.controlType == PLAYER_CONTROL.Free ? false : true;
		buttonCursorFollow.interactable = playerControl.controlType == PLAYER_CONTROL.CursorFollow ? false : true;
	}

}
