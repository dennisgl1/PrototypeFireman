using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {
	private static PlayerData instance = null;
	public static PlayerData Instance{
		get{ return instance; }
	}
}
