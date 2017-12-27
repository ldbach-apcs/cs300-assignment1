using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacebookQuestInput :  IQuestInput {


	public static string PREV_SHARE = "prev_share";
	public static string INPUT_SHARE = "input_share";

	private int prevShare = 0;
	private int totalShare = 0;


	public FacebookQuestInput()
	{
		prevShare = PlayerPrefs.GetInt(PREV_SHARE, 0);
		totalShare = prevShare;
	}

	/*
     * This function is used to handle share reading
     */
	public void OnShare(bool shareSucessful) 
	{
		totalShare += shareSucessful? 1 : 0;
		// Save share progress
		PlayerPrefs.SetInt(PREV_SHARE, totalShare);

		var data = new QuestInputData(INPUT_SHARE);
		data.PutValue(totalShare);
		Notify(data);
	}

	override public void Init() {
		OnShare(false);
	}

	public void Destroy()
	{
		// save the current share number
		PlayerPrefs.SetInt(PREV_SHARE, totalShare);
	}
}
