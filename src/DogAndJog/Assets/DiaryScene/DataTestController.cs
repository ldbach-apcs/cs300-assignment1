using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataTestController : MonoBehaviour {
	void Start() {
		TestDatabase();
	}

	void Update() {

	}

	private void TestDatabase() {
		// IQuest currentQuest = DatabaseReader.Instance().ReadQuest();
		QuestFactory.Instance().GetQuest();
	}
}
