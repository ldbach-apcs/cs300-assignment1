using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataTestController : MonoBehaviour {
	public Text questTitle, questDescription, questProgress, distanceTotalDisplay;
	
	void Update() {
		IQuest curQuest = new QuestManager(null).GetQuest();
		questTitle.text = curQuest.name;
		questDescription.text = curQuest.description;
		questProgress.text = curQuest.GetProgress();
		distanceTotalDisplay.text = DatabaseReader.Instance().totalDistance.ToString();
	}
}
