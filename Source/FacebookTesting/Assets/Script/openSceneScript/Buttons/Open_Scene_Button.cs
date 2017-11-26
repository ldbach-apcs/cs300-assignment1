using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class Open_Scene_Button : MonoBehaviour {
	public string sceneName;
	// Use this for initialization
	public void Create(){
	}
	public void OnMouseDown () {
		SceneManager.LoadScene(sceneName);
	}
}