using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacebookQuestInput :  IQuestInput {


	private bool isShare = false;
	public static string SHARE_STATUS = "share_status";
	public static string INPUT_SHARE = "input_share";


	public FacebookQuestInput()
	{
		isShare = PlayerPrefs2.GetBool (SHARE_STATUS);
	}

	/*
     * This function is used to handle share reading
    
     */
	private void OnShare(bool share) 
	{
		isShare = share;
		var data = new QuestInputData (INPUT_SHARE); 
		Notify(data);
	}

	public void Destroy()
	{
		// save the current step state
		PlayerPrefs2.SetBool(SHARE_STATUS, isShare);
	}
}
