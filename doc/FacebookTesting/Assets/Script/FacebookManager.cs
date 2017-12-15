using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;
using System;

public class FacebookManager : MonoBehaviour {
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
			AppLinkUrl = "http://google.com";
		}
	}
	public void Share()
	{
		FB.FeedShare (String.Empty,
			new Uri(AppLinkUrl),//applink
			"D&J is f*cking awesome!",//caption
			"This is my pet. It is f*cking cute, right?",//title
			"Guys! Check this game!",//description
			new Uri("http://cdn.akc.org/Marketplace/Breeds/Pembroke_Welsh_Corgi_SERP.jpg"), //Url for an image. or potentially put a screenshot in there
			String.Empty, // this one is for audio/video
			ShareCallBack
		);
	}

	void ShareCallBack (IShareResult result)
	{
		//check if is an error or success
		if (result.Cancelled) {
			Debug.Log ("Share canceled");
		} else if (!String.IsNullOrEmpty (result.Error)) {
			Debug.Log ("Errors on share!");	
		}
		else if (!String.IsNullOrEmpty (result.RawResult)) {
			Debug.Log ("Success on Share!");
		}
			
	}
	public void Invite()
	{
		FB.Mobile.AppInvite(new Uri(AppLinkUrl),// app link
		new Uri("http://cdn.akc.org/Marketplace/Breeds/Pembroke_Welsh_Corgi_SERP.jpg"),//image
			InviteCallBack);
	}

	void InviteCallBack (IAppInviteResult result)
	{
		if (result.Cancelled) {
			Debug.Log ("Invite canceled");
		} else if (!String.IsNullOrEmpty (result.Error)) {
			Debug.Log ("Errors on invite!");	
		}
		else if (!String.IsNullOrEmpty (result.RawResult)) {
			Debug.Log ("Success on invite!");
		}
	}
	//share when alr your friends have this app.
	public void ShareWithUsers()
	{
		FB.AppRequest ("Walk with me together, my friend",//message
			null,
			//list of appusers
			new List<object>(){"app_users"},//target app user
			null,// max recipients
			null,
			null,
			null,
			ShareWithUsersCallback);
	}
	void ShareWithUsersCallback(IAppRequestResult result)//invite who alr have our app
	{
		if (result.Cancelled) {
			Debug.Log ("Request canceled");
		} else if (!String.IsNullOrEmpty (result.Error)) {
			Debug.Log ("Errors on request!");	
		}
		else if (!String.IsNullOrEmpty (result.RawResult)) {
			Debug.Log ("Success on request!");
		}
	}
}
