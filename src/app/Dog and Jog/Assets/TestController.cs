using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestController : MonoBehaviour, IQuestObserver {

	public Text quest, distance, reward;

	private DistanceQuestInput questInput;
	private QuestManager questManager;

	void Start()
	{
		// Load input device
		questInput = new DistanceQuestInput();
		questInput.Register (this);

		// Init quest manager
		questManager = new QuestManager();
	}

//	void Update()
//	{
//
//	}

	public void OnQuestInput(QuestInputData data)
	{
		questManager.UpdateQuest (data);

		// This is only for testing*
		quest.text = questManager.GetQuest ().ToString ();
		distance.text = (data.GetValue (DistanceQuestInput.INPUT_DISTANCE)).ToString();

		if (questManager.QuestIsFinish ()) {
			reward.text = "Rewarded!";
		}
	}

	public void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus) {
			// Clean up input device
			questInput.Destroy ();
			questManager.SaveQuest ();
		} else {
			

		}
	}
}
