using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour {

	public AudioClip clip;

	void Awake()
	{
		GetComponent<AudioSource> ().clip = clip;
		GetComponent<AudioSource> ().time = 2.5f;
		GetComponent<AudioSource> ().Play ();
		DontDestroyOnLoad (transform.gameObject);
	}
}
