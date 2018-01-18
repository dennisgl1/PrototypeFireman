using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
	public delegate void GameOver();
	public event GameOver OnGameOver;

	[Header("UI")]
	public Text textScore;
	public Text textLife;

	[Header("Modify this")]
	public int startLife;

	[Header("DON'T MODIFY")]
	public int score;
	public int life;

	public void Init()
	{
		SetScore(0);
		SetLife(startLife);
	}

	public void RegisterBounceObjectEvent(BounceObject bounceObject)
	{
		bounceObject.OnBounce += AddScore;
		bounceObject.OnFall += DecreaseLife;
	}

	public void UnRegisterBounceObjectEvent(BounceObject bounceObject)
	{
		bounceObject.OnBounce -= AddScore;
		bounceObject.OnFall -= DecreaseLife;
	}

	void AddScore()
	{
		ModScore(1);
	}

	void DecreaseLife(BounceObject g)
	{
		ModLife(-1);
	}

	public void SetScore(int value)
	{
		score = value;
		UpdateScore();
	}
	public void ModScore(int value)
	{
		score += value;
		UpdateScore();
	}
	public int GetScore()
	{
		return score;
	}

	public void SetLife(int value)
	{
		life = value;
		UpdateLife();
	}
	public void ModLife(int value)
	{
		life += value;
		UpdateLife();

		if(life <= 0){
			if(OnGameOver != null) OnGameOver();
		}
	}
	public int GetLife()
	{
		return life;
	}

	void UpdateScore()
	{
		textScore.text = score.ToString();
	}

	void UpdateLife()
	{
		textLife.text = "Life: "+life.ToString();
	}
}