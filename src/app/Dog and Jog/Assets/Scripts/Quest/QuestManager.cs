using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager {
    private IQuest currentQuest;

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

    // Return type of this function will be changed
    public void RewardInfo()
    {
        // return currentQuest.reward;
    }
}
