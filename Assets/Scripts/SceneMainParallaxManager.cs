using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMainParallaxManager : MonoBehaviour {
	public PanelCountdown panelCountdown;
	public PanelGameOver panelGameOver;
	public PlayerControl playerControl;
	public SpawnPointParent spawnPointParent;
	public BounceObjectParent bounceObjectParent;
	public HUD hud;
	public Road[] roads;

	public void Start()
	{
		panelGameOver.Hide();

		hud.OnGameOver += GameOver;
		hud.Init();

		playerControl.Init();
		InitEnvironment();
		spawnPointParent.Init();

		if(SceneManager.GetActiveScene().name != "Simulation"){
			panelCountdown.OnGo += OnGo;
			panelCountdown.Init();
		}
	}

	public void Reset()
	{
		panelGameOver.Hide();

		hud.Init();

		playerControl.Init();
		InitEnvironment();
		spawnPointParent.Init();

		panelCountdown.Init();
	}

	void OnGo ()
	{
		spawnPointParent.flagSpawning = true;
	}

	void GameOver()
	{
		spawnPointParent.Stop();
		bounceObjectParent.DestroyObjects();
		StopEnvironment();
		panelGameOver.Show();
	}

	void InitEnvironment()
	{
		foreach(Road r in roads){
			r.flagMoving = true;
		}
	}

	void StopEnvironment()
	{
		foreach(Road r in roads){
			r.flagMoving = false;
		}
	}
}