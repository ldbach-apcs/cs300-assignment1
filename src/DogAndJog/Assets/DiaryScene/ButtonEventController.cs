using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonEventController : MonoBehaviour {

	public GameObject achievementView, diaryView;

	public void Button_ToAchivement_OnClick()
	{
		// Disable DiaryView, switch to AchievementView
		// diaryView.SetActive(false);
		// achievementView.SetActive(true);


		SceneManager.LoadScene("Main");
	}

	public void Button_ToDiary_OnClick()
	{
		// Disable Achievement view, switch to Diary View
		diaryView.SetActive(true);
		achievementView.SetActive(false);
	}

	public void Button_Share() {
		Debug.Log(DatabaseReader.Instance().totalShare);

		var fbInstance = FacebookManager.Instance;
		fbInstance.InitFB();
		
		Debug.Log(DatabaseReader.Instance().totalShare);
	}
}
