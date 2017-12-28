using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefs2 : MonoBehaviour {

public static void SetBool(string key,bool state)
	{
		PlayerPrefs.SetInt (key, state ? 1 : 0);
	}	
	public static bool GetBool(string key)
	{
		int value = PlayerPrefs.GetInt (key);
		return value == 1 ? true : false;
	}
}
