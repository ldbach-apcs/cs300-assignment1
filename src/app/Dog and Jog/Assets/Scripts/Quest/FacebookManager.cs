using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;
using System;

public class FacebookManager : MonoBehaviour {

	private FacebookQuestInput facebookQuestInput;


	//a singleton for facebook Manager
	//backend
	private static FacebookManager _instance;
	public static FacebookManager Instance
	{
		get{
			if(_instance == null)
			{
				GameObject fbm = new GameObject ("FBManager");
				fbm.AddComponent<FacebookManager>();
			}
			return _instance;
		}
	}
	public bool isLoggedIn{ get; set;}// get set the variable can only be used in a function
	public string ProfileName {get;set;}//some properties of get; set;
	public Sprite ProfilePic {get;set;}
	public string AppLinkUrl { get; set;}
	public bool isShared;
	void Awake()
	{
		DontDestroyOnLoad (this.gameObject);//does not destroy when moving to other scences)/change scence still avaiable
		//now can facebook manager instead of constantly do facebook API request.
		_instance = this;
	}
	public void InitFB()
	{
		if (!FB.IsInitialized) {
			FB.Init (SetInit, OnHideUnity);// init when logged out.
		} else {
			isLoggedIn = FB.IsLoggedIn; //updating facebook status
		}
		isShared = false;
	}

	private void OnHideUnity(bool isGameShown)
	{
		if (!isGameShown)
		{
			Time.timeScale = 0;
		}
		else {
			Time.timeScale = 1;
		}
	}

	private void SetInit()
	{
		Debug.Log ("FB Init done");
		if (FB.IsLoggedIn) {
			Debug.Log ("FB loggin.");
			getProfile();
		} 
		else 
		{
			Debug.Log ("FB is not loggin.");
		}
		isLoggedIn = FB.IsLoggedIn;
	}
	public void getProfile()
	{
		FB.API ("/me?fields=first_name", HttpMethod.GET, DisplayUsername);
		FB.API ("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfilePic);
		//we want to invite friends into our application so we need our application link from a function
		FB.GetAppLink(DealWithAppLink);//will cause problem when run on a phone.

	}
	void DisplayUsername (IGraphResult result)
	{
		if (result.Error ==null) {
			ProfileName = "" + result.ResultDictionary ["first_name"]; //get from index first_name make it more of a string
		}
		else {
			Debug.Log (result.Error);
		}
	}

	void DisplayProfilePic (IGraphResult result)
	{
		if(result.Texture !=null)//check if texture return
		{
			if (result.Error ==null) {
				ProfilePic = Sprite.Create(result.Texture,new Rect(0,0,128,128),new Vector2());//API doesnot return sprite so cast the texture
				//vector2?
			}	
		}
		else {
			Debug.Log (result.Error);
		}
	}

	void DealWithAppLink (IAppLinkResult result)
	{
		if (!String.IsNullOrEmpty (result.Url)) {
			AppLinkUrl = result.Url;
		} else {
			AppLinkUrl = "http://n3k.ca/";
		}
	}
	public void Share() // no longer support caption description or title
	{
		if (AppLinkUrl == null) {
			AppLinkUrl = "http://n3k.ca/";
		}
	
		FB.ShareLink(new Uri(AppLinkUrl),
			"This is my pet. It is f*cking cute, right?",
			"Guys! Check this game!",
			new Uri("http://cdn.akc.org/Marketplace/Breeds/Pembroke_Welsh_Corgi_SERP.jpg"),
			ShareCallBack);
	}


	void ShareCallBack (IShareResult result)
	{
		//check if is an error or success
		if (result.Cancelled) {
			facebookQuestInput.OnShare(false);
			Debug.Log ("Share canceled");
		} else if (!String.IsNullOrEmpty (result.Error)) {
			Debug.Log ("Errors on share!");	
		}
		else if (!String.IsNullOrEmpty (result.RawResult)) {
			isShared = true;
			// Text mission = FBScript.getInstance().DialogMission.GetComponent<Text> ();
			// mission.text = "Share time: 1/1";
			// Debug.Log ("Success on Share!" + isShared.ToString());
			facebookQuestInput.OnShare(isShared);
		}

	}

	public void register(FacebookQuestInput input) {
		this.facebookQuestInput = input;
	}

}
