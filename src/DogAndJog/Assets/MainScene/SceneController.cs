using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {

	public void LoadScene(string Scene){
		Application.LoadLevel (Scene);
	}

	public void OpenMenu(GameObject obj){
		obj.SetActive (!obj.activeInHierarchy);
	}
}
