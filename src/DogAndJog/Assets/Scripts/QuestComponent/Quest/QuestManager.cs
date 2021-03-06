﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager {
    private static IQuest currentQuest = null;

	public QuestManager(IQuest _currentQuest) 
	{
		if (currentQuest == null)
			currentQuest = _currentQuest;
	}

	public void SetQuest (IQuest quest) {
		currentQuest = null;
		currentQuest = quest;
	}


	public void UpdateQuest(QuestInputData data)
    {
		currentQuest.Update (data);
		// FinishQuest();
    }

    private void FinishQuest()
    {
		if (currentQuest.IsFinish ()) 
		{
			// Claim Reward
			// Geneerate new Quest
			currentQuest = null;
			currentQuest = QuestFactory.Instance().GetQuest();
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

	// Load Quest will be changed when adding Database Component
	private IQuest LoadQuest()
	{
		DatabaseReader dbReader = DatabaseReader.Instance();
		return dbReader.ReadQuest();
	}
}
