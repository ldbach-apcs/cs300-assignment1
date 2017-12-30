using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour {

	//[SerializeField]
	//public int scene;

	public Scene scene;
	public Text loading;

	// Use this for initialization
	void Start () {
		 StartCoroutine(LoadNewScene());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	 IEnumerator LoadNewScene() {

        // This line waits for 3 seconds before executing the next line in the coroutine.
        // This line is only necessary for this demo. The scenes are so simple that they load too fast to read the "Loading..." text.
		MasterController.Instance().Init();
        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        // AsyncOperation async = SceneManager.LoadSceneAsync("Main");
		yield return new WaitForSeconds(0.5f);
		loading.text = "Loading.";
		yield return new WaitForSeconds(0.5f);
		loading.text = "Loading..";
		yield return new WaitForSeconds(0.5f);
		loading.text = "Loading...";
		yield return new WaitForSeconds(0.5f);
		loading.text = "Loading....";
		yield return new WaitForSeconds(0.5f);
		loading.text = "Loading.....";

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
//        while (!async.isDone) {
//            yield return null;
//        }

	}
}
