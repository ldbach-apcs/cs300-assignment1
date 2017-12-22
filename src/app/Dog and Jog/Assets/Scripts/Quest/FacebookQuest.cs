using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacebookQuest : IQuest {

	private bool isShare;

	private bool curShare;
	private bool isDone = false;

	public FacebookQuest
	(string name, string description, bool isShare) : base(name, description)
	{
		this.curShare = PlayerPrefs2.GetBool (FacebookQuestInput.SHARE_STATUS);
		this.isShare = isShare;
		this.action = "Share on Facebook";
	}


	public override void Update(QuestInputData data)
	{
		if (FacebookManager.Instance.isShared ==true)
			isDone = true; 

	}

	public override bool IsFinish()
	{
		return isDone;
	}

	public override void Save()
	{
		PlayerPrefs.SetString ("test_name_share", name);
		PlayerPrefs.SetString ("test_des_share", description);
		PlayerPrefs2.SetBool ("test_req_share", isShare);
	}

	public override string GetProgress() 
	{
		if (isShare == true)
			return "Share: 1/1";
		else return "Share: 0/1";
	}
}
