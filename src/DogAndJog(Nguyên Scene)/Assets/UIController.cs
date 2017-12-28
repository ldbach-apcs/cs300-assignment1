using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public float x;
	public float y;

	void Update(){
		UpdateAffectionUI ();
		UpdateHungerUI();
	}

	void UpdateHungerUI(){
		RectTransform bar = GameObject.Find ("Hunger").gameObject.GetComponent<RectTransform>();
		RectTransform holder = GameObject.Find ("HungerBar").gameObject.GetComponent<RectTransform>();
		bar.sizeDelta = new Vector2 (holder.sizeDelta.x * GetHunger(), bar.sizeDelta.y);
		bar.localPosition = - new Vector2 (holder.sizeDelta.x * (1- GetHunger()) / 2, bar.localPosition.y);
	}
	void UpdateAffectionUI(){
		RectTransform bar = GameObject.Find ("Affection").gameObject.GetComponent<RectTransform>();
		RectTransform holder = GameObject.Find ("AffectionBar").gameObject.GetComponent<RectTransform>();
		bar.sizeDelta = new Vector2 (holder.sizeDelta.x * GetAffection(), bar.sizeDelta.y);
		bar.localPosition = - new Vector2 (holder.sizeDelta.x * (1- GetAffection()) / 2, bar.localPosition.y);
	}

	string GetLevel(){
		return "5";
	}
	float GetHunger(){
		return x;
	}
	float GetAffection(){
		return y;
	}

}
