using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacebookQuest : IQuest {

	private int shareStart;
	private int shareRequire;
	private int shareTotal;
	private bool isDone = false;

	public FacebookQuest
	(string name, string description, int shareStart, int shareRequire) : base(name, description)
	{
		isDone = false;
		this.shareRequire = shareRequire;
		this.shareStart = shareStart;
		shareTotal = shareStart;
	}


	public override void Update(QuestInputData data)
	{
		shareTotal = (int) data.GetValue(FacebookQuestInput.INPUT_SHARE);
		isDone = (shareStart + shareRequire <= shareTotal);


		Debug.Log(shareStart.ToString() + " " + shareTotal.ToString());
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
		int remainingShare = shareTotal - shareStart;
		if (isDone) return "Shared: " + shareRequire.ToString() + " / " + shareRequire.ToString();
		else return "Shared: " + remainingShare.ToString() + " / " + shareRequire.ToString();
	}
}
