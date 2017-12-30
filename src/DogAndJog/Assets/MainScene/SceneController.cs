using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {

	public GameObject musicPlayer;
	public GameObject canvas;
	int curMin;

	void Start(){
		curMin = PlayerPrefs.GetInt ("curMin");

		GameObject musicObject = GameObject.FindWithTag("Music");
		if (musicObject == null) {
			Instantiate(musicPlayer, Vector3.zero, Quaternion.identity);
		}
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
	public void Eat(int index){
		if (DatabaseReader.Instance ().hunger < 1440) {
			//DatabaseReader.Instance ().ReadFood () [index].quanity = DatabaseReader.Instance ().ReadFood () [index].quanity - 1;
			DatabaseReader.Instance ().hunger = DatabaseReader.Instance ().hunger + 40;
			canvas.GetComponent<UIController> ().UpdateAffectionUI ();
			canvas.GetComponent<UIController> ().UpdateHungerUI ();
			canvas.GetComponent<UIController> ().UpdateLevelUI ();
		}
		if (DatabaseReader.Instance ().hunger >= 1440) {
			DatabaseReader.Instance ().hunger = 1440;
		}
	}

	private void DecreaseHungerOT(int speed){
		long diff = System.DateTime.Now.Date.Minute - curMin;
		UpdatecurMin ();
		if (diff == 0) {
			return;
		}
		else {
			int hunger = DatabaseReader.Instance().hunger;
			if (hunger - speed <= 0) {
				DatabaseReader.Instance().hunger = 0;
			} else {
				DatabaseReader.Instance().hunger -= speed;
			}
		}
	}
	private void DecreaseAffectionOT(int speed){
		long diff = System.DateTime.Now.Date.Minute - curMin;
		UpdatecurMin ();
		if (diff == 0) {
			return;
		}
		else {
			int exp = DatabaseReader.Instance().exp;
			if (exp - speed <= 0) {
				DatabaseReader.Instance().exp = 0;
			} else {
				DatabaseReader.Instance().exp -= speed;
			}
		}
	}
	private void UpdatecurMin(){
		if (System.DateTime.Now.Date.Minute != curMin)
			PlayerPrefs.SetInt ("curMin", System.DateTime.Now.Date.Minute);
	}

}
