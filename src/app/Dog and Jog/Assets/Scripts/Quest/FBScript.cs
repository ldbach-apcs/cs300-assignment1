using Facebook.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class FBScript : MonoBehaviour {
	//front-end
	public GameObject DialogLoggedIn; //log in
	public GameObject DialogLoggedOut;// log out
	public GameObject DialogUsername;
	public GameObject DialogProfilePic;
	public  GameObject DialogMission;
	// Use this for initialization
	void Awake () {
		FacebookManager.Instance.InitFB();
		DealWithFBMenu (FB.IsLoggedIn);
	}
	private static FBScript Instance = null;
	public static FBScript getInstance()
	{
		if (Instance == null) {
			Instance = (FBScript)	FindObjectOfType (typeof(FBScript));

		}
		return Instance;

	}

	public void FBLogin()
	{
		List<string> permissions = new List<string> ();//list of permissions
		permissions.Add ("public_profile");
		FB.LogInWithReadPermissions (permissions, callback);

	}
	public void FBLogout()
	{
		FB.LogOut ();
		DealWithFBMenu (FB.IsLoggedIn);
	}
	void callback (ILoginResult result)//IResult to be call when the result complete
	{
		if (result.Error != null) {
			Debug.Log (result.Error);
		} 
		else 
		{
			if (FB.IsLoggedIn) {// when logged in successfully
				FacebookManager.Instance.isLoggedIn = true;
				FacebookManager.Instance.getProfile ();
				Debug.Log ("FB loggin.");
			} 
			else 
			{
				Debug.Log ("FB is not loggin.");
			}
		}
		DealWithFBMenu (FB.IsLoggedIn);//loggedin	
	}
	void DealWithFBMenu(bool isLoggedIn)
	{
		if (isLoggedIn) {
			DialogLoggedIn.SetActive (true);
			DialogLoggedOut.SetActive (false);
			if (FacebookManager.Instance.ProfileName != null) {
				Text userName = DialogUsername.GetComponent<Text> ();
				userName.text = "Hi, " + FacebookManager.Instance.ProfileName;
			} else {
				StartCoroutine("WaitForProfileName");
			}
			if (FacebookManager.Instance.ProfilePic != null) {
				Image profilePic = DialogProfilePic.GetComponent<Image> ();
				profilePic.sprite = FacebookManager.Instance.ProfilePic;
			} else {
				StartCoroutine("WaitForProfilePic");
			}
		}
		else {
			DialogLoggedOut.SetActive (true);
			DialogLoggedIn.SetActive (false);
		}
	}
	//take a little while to comeback
	//use something call a co-routine only called update when it is required to happen
	IEnumerator WaitForProfileName()
	{
		while (FacebookManager.Instance.ProfileName == null) {
			yield return null; // if is not set then wait for a frame and then come back to run in al oop
		}
		DealWithFBMenu(FacebookManager.Instance.isLoggedIn);
	}
	IEnumerator WaitForProfilePic()
	{
		while (FacebookManager.Instance.ProfilePic == null) {
			yield return null; // if is not set then wait for a frame and then come back to run in al oop
		}
		DealWithFBMenu(FacebookManager.Instance.isLoggedIn);
	}
	// Share when acheive an achievement or something.

	public void Share()
	{
		FacebookManager.Instance.Share ();
	}


}