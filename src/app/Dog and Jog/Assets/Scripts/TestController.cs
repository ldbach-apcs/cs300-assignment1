using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestController : MonoBehaviour , IQuestObserver {

	public Text quest, total, remaining;

	// private IQuestInput questInput;
	private QuestManager questManager;
	private IQuestInput facebookQuestInput;

	void Start()
	{
		// Reset PlayerPref for testing
		PlayerPrefs.DeleteAll();

		// Load input device
		// questInput = new DistanceQuestInput();
		// questInput.Register(observer: this);

		facebookQuestInput = new FacebookQuestInput ();
		facebookQuestInput.Register (observer: this);

		FacebookManager.Instance.register((FacebookQuestInput) facebookQuestInput);
		// Init quest manager
		questManager = new QuestManager();

		// Init textUI with starter data
		facebookQuestInput.Init();
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
		// int inputDistance = (int) data.GetValue (DistanceQuestInput.INPUT_DISTANCE);
		// distance.text = inputDistance.ToString();

		int curShare = (int) data.GetValue(FacebookQuestInput.INPUT_SHARE);
		total.text = curShare.ToString();

		// remaining
		remaining.text = curQuest.GetProgress();
	}

	public void OnApplicationPause(bool pauseStatus)
	{
	}
}
