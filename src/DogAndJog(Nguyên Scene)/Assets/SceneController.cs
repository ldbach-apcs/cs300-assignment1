using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {

	int curMin;

	void Start(){
		curMin = PlayerPrefs.GetInt ("curMin");
	}

	void Update(){
		DecreaseHungerOT (1 / 1440);
		DecreaseHungerOT (1 / 1440);
		PlayerPrefs.SetInt ("curMin", System.DateTime.Now.Date.Minute);
	}

	public void LoadScene(string Scene){
		Application.LoadLevel (Scene);
	}
	public void OpenMenu(GameObject obj){
		obj.SetActive (!obj.activeInHierarchy);
	}

	private void DecreaseHungerOT(float speed){
		long diff = System.DateTime.Now.Date.Minute - curMin;
		UpdatecurMin ();
		if (diff == 0) {
			return;
		}
		else {
			float decrease = diff * speed;
			float hunger = PlayerPrefs.GetFloat ("Hunger");
			if (hunger - decrease <= 0) {
				PlayerPrefs.SetFloat ("Hunger", 0);
			} else {
				PlayerPrefs.SetFloat ("Hunger", hunger - decrease);
			}
		}
	}
	private void DecreaseAffectionOT(float speed){
		long diff = System.DateTime.Now.Date.Minute - curMin;
		UpdatecurMin ();
		if (diff == 0) {
			return;
		}
		else {
			float decrease = diff * speed;
			float hunger = PlayerPrefs.GetFloat ("Affection");
			if (hunger - decrease <= 0) {
				PlayerPrefs.SetFloat ("Affection", 0);
			} else {
				PlayerPrefs.SetFloat ("Affection", hunger - decrease);
			}
		}
	}
	private void UpdatecurMin(){
		if (System.DateTime.Now.Date.Minute != curMin)
			PlayerPrefs.SetInt ("curMin", System.DateTime.Now.Date.Minute);
	}

}
