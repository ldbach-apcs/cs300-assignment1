using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager {
    private IQuest currentQuest;

	public QuestManager() 
	{
		currentQuest = LoadQuest ();

		if (currentQuest == null) {
			currentQuest = QuestFactory.Instance ().GetQuest ();
		}
	}


	public void UpdateQuest(QuestInputData data)
    {
		currentQuest.Update (data);


    }

    public void FinishQuest()
    {
		if (currentQuest.IsFinish ()) 
		{
			// Claim Reward

			// Geneerate new Quest
		}
    }

	public IQuest GetQuest()
	{
		return currentQuest;
	}

	public bool QuestIsFinish()
	{
		return currentQuest.IsFinish ();
	}

    // Return type of this function will be changed
    public void RewardInfo()
    {
        // return currentQuest.reward;
    }

	public void SaveQuest()
	{
		currentQuest.Save ();
	}

	private IQuest LoadQuest()
	{
		string title = PlayerPrefs.GetString ("test_name", null);
		string description = PlayerPrefs.GetString ("test_des", null);
		float disReq = PlayerPrefs.GetFloat ("test_req", -1);
		float disStart = PlayerPrefs.GetFloat ("test_start", -1);

		if (title == null)
			return null;
		return new DistanceQuest (title, description, disReq, disStart);
	}
}
