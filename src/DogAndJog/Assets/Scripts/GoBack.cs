using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GoBack : MonoBehaviour {

	void Update() {
		if (Input.GetKey(KeyCode.Escape)) {
			// Reload main scene
			SceneManager.LoadScene("Main");
		}
	}
}
