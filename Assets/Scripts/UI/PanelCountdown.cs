using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelCountdown : MonoBehaviour {
	public delegate void Go();
	public event Go OnGo;

	public Animator textCountdownAnim;
	public Text textCountdown;

	public void Init()
	{
		gameObject.SetActive(true);
		StartCoroutine(StartCountdown());
	}

	IEnumerator StartCountdown()
	{
		for(int i = 3;i>0;i--){
			textCountdown.text = (i).ToString();
			textCountdownAnim.SetTrigger("Animate");
			yield return new WaitForSeconds(1f);
		}

		textCountdown.text = "GO!";
		yield return new WaitForSeconds(1f);

		gameObject.SetActive(false);
		if(OnGo != null) OnGo();
		yield return null;
	}

}
