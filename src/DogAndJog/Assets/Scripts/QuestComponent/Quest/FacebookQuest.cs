using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacebookQuest : IQuest {

	
	private int shareTotal;
	private bool isDone = false;

	public FacebookQuest
	(string name, string description, int shareStart, int shareRequire, int rewardExp, int rewardMoney) : base(name, description, rewardExp, rewardMoney)
	{
		isDone = false;
		requireValue = shareRequire;
		prevValue = shareStart;
		shareTotal = (int) prevValue;
		action = "Share";
	}


	public override void Update(QuestInputData data)
	{
		shareTotal = (int) data.GetValue(FacebookQuestInput.INPUT_SHARE);
		// Not correct input method
		if (shareTotal == 0) return;
		isDone = (prevValue + requireValue <= shareTotal);


		// Debug.Log(shareStart.ToString() + " " + shareTotal.ToString());
	}

	public override bool IsFinish()
	{
		return isDone;
	}

	public override void Save()
	{
		PlayerPrefs.SetString ("test_name_share", name);
		PlayerPrefs.SetString ("test_des_share", description);
	}

	public override string GetProgress() 
	{
		int remainingShare = shareTotal - (int) prevValue;
		if (isDone) return "Shared: " + ((int) requireValue).ToString() + " / " + ((int) requireValue).ToString();
		else return "Shared: " + remainingShare.ToString() + " / " + ((int) requireValue).ToString();
	}

	public override string ToString() {
		return action + " " + 	((int) requireValue).ToString() + " times";
	}
}
