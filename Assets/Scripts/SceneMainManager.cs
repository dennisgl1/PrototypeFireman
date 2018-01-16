using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class SceneMainManager : MonoBehaviour {
	[Header("Reference Attributes")]
	public GameObject prefabBounceObject;

	[Header("UI Attributes")]
	public Text textCountdown;
	public RectTransform heartParent;
	public GameObject prefabUIHeart;
	public GameObject panelStart;
	public Text textScore;
	public GameObject panelGameOver;
	public Text textTotalScore;

	public float heartStartX, heartNextX;

	[Header("Vairable Attributes")]
	public float startingLife = 3f;

	public float objectSpawnInterval = 10f;
	public float[] spawnPosY;

	[Header("DO NOT MODIFY")]
	public List<GameObject> spawnedObjects;
	public List<GameObject> heartObjects;

	bool spawning = false;
	float t;
	int score = 0;
	int totalObject;

	public void RestartGame(){
		StartCoroutine(Start());
	}

	//START
	IEnumerator Start()
	{
		panelGameOver.SetActive(false);
		InitLife();
		SetScore(0);
		panelStart.SetActive(true);
		t = objectSpawnInterval;

		for(int i = 3;i>0;i--){
			textCountdown.text = (i).ToString();
			textCountdown.GetComponent<Animator>().SetTrigger("Animate");
			yield return new WaitForSeconds(1f);
		}

		textCountdown.text = "GO!";
		yield return new WaitForSeconds(1f);

		panelStart.SetActive(false);

		SpawnObject();
		spawning = true;
		yield return null;
	}

	void InitLife()
	{
		for(int i = 0;i<startingLife;i++){
			heartObjects.Add(Instantiate(prefabUIHeart,heartParent));
			heartObjects[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(GetHeartXPos(i),heartObjects[i].GetComponent<RectTransform>().anchoredPosition.y);
		}
	}
		
	float GetHeartXPos(int index)
	{
		return heartStartX + (index*heartNextX);
	}

	void SetScore(int score)
	{
		this.score = score;
		textScore.text = score.ToString();
	}

	void ModScore(int value)
	{
		this.score += value;
		textScore.text = this.score.ToString();
	}

	void Update()
	{
		ValidateSpawning();
	}

	void ValidateSpawning()
	{
		if(spawning){
			t -= Time.deltaTime;
			if(t <= 0f){
				SpawnObject();
				t = objectSpawnInterval;
			}
		}
	}

	void SpawnObject()
	{
//		int rnd = UnityEngine.Random.Range(0,spawnPosY.Length);
//		if(totalObject <= 1) rnd = 0;
		int rnd = 0;

		float yPos = spawnPosY[rnd];


		GameObject tempBounceObject = Instantiate(prefabBounceObject,new Vector3(-8.5f,yPos,0f),Quaternion.identity);
		BounceObject bounceObject = tempBounceObject.GetComponent<BounceObject>();
		bounceObject.OnFall += DecreaseLife;
		bounceObject.OnBounce += AddScore;
		bounceObject.OnFinish += BounceObjectOnFinish;
		spawnedObjects.Add(tempBounceObject);

		bounceObject.Init(rnd);

		totalObject++;
		ValidateSpawnInterval();
	}

	void BounceObjectOnFinish (BounceObject g)
	{
		g.OnFinish -= DecreaseLife;
		g.OnBounce -= AddScore;
		g.OnFall -= DecreaseLife;
	}

	void ValidateSpawnInterval()
	{
		if(totalObject % 5 == 0){
			if(objectSpawnInterval > 3f){
				objectSpawnInterval -= 0.8f;
			}
		}
	}

	void AddScore()
	{
		ModScore(1);
	}

	void DecreaseLife(BounceObject g)
	{
		if(heartObjects.Count <= 0){
			//gameover
			GameOver();
		}else{
			Destroy(heartObjects[heartObjects.Count-1]);
			heartObjects.RemoveAt(heartObjects.Count-1);

			if(heartObjects.Count <= 0){
				GameOver();
			}
		}

		g.OnFall -= DecreaseLife;
		spawnedObjects.Remove(g.gameObject);
		Destroy(g.gameObject);
	}

	void GameOver()
	{
		spawning = false;
		if(spawnedObjects.Count > 0){
			foreach(GameObject g in spawnedObjects){
				if(g != null){
					g.GetComponent<BounceObject>().OnFall -= DecreaseLife;
					g.GetComponent<BounceObject>().OnBounce -= AddScore;
					Destroy(g);
				}
			}
		}
		spawnedObjects.Clear();

		panelGameOver.SetActive(true);
		textTotalScore.text = this.score.ToString();
	}
}
