using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestController : MonoBehaviour, IQuestObserver {

	public Text quest, distance, remaining;

	private DistanceQuestInput questInput;
	private QuestManager questManager;

	void Start()
	{
		// Load input device
		questInput = new DistanceQuestInput();
		questInput.Register(observer: this);

		// Init quest manager
		questManager = new QuestManager();

		// Init textUI with starter data
		questInput.Init();
	}

//	void Update()
//	{
//
//	}

	public void OnQuestInput(QuestInputData data)
	{
		questManager.UpdateQuest(data);

		// This is only for testing*
		IQuest curQuest = questManager.GetQuest();
		quest.text = curQuest.name;
		int inputDistance = (int) data.GetValue (DistanceQuestInput.INPUT_DISTANCE);
		distance.text = inputDistance.ToString();

		// remaining
		remaining.text = curQuest.GetProgress();
	}

	public void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus) {
			// Clean up input device
			questInput.Destroy ();
			questInput = null;
			questManager.SaveQuest ();
			questManager = null;
		} else {
			questInput = new DistanceQuestInput();
			questInput.Register (this);
			questManager = new QuestManager();
			questInput.Init ();			
		}
	}
}
