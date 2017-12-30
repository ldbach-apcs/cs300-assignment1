using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBehavior : MonoBehaviour {

	int level;
	int hunger;
	Vector3 from = new Vector3 (0, 0, 0);
	Vector3 des = new Vector3(0,2.56f,19.09f);
	string state = "sitting";
	int frames = 60;
	List<string> emotions;

	// Use this for initialization
	void Start () {
		//GameObject.Find("DogMesh").GetComponent<Renderer>().materials[0].SetTexture("Base(RGB)",
		emotions = new List<string>();
		DisplayEmotion ();
	}
	
	// Update is called once per frame
	void Update () {
		DogAction ();
	}

	bool ActionState(string state, int frames){
		switch (state) {
		case "sitting":
			GetComponent<Animation> ().Play ("Sitting");
			return true;
		case "coming":
			if (this.transform.position.z >= des.z) {
				this.transform.position = des;
				if (GetComponent<Animation> () ["Standing Up"].normalizedTime < 0.9) {
					GetComponent<Animation> ().Play ("Standing Up");
					GetComponent<AudioSource> ().Play ();
					return false;
				} else {
					return true;
				}
			} else {
				GetComponent<Animation> ().Play ("Running");
				this.transform.Translate(new Vector3(0,0,des.z - from.z)/frames);
				return false;
			}
		case "wiggling":
			GetComponent<Animation> ().Play ("Wiggling");
			return true;
		case "return":
			if (this.transform.position.z <= 0) {
				this.transform.position = from;
				this.transform.forward = -this.transform.forward ;
				GetComponent<Animation> ().Play ("Sitting");
				return true;
			} else {
				GetComponent<Animation> ().Play ("Running");
				this.transform.position = new Vector3 (0, 0, this.transform.position.z);
				this.transform.Translate(new Vector3(0,0,des.z - from.z)/frames);
				return false;
			}
		default:
			return false;
		}
	}
	void DogAction(){
		if (ActionState (state, 60)) {
			switch (state) {
			case "sitting":
				if (TouchDog()) {
					DisplayEmotion ();
					state = "coming";
				}
				break;
			case "coming":
				state = "wiggling";
				break;
			case "wiggling":
				if (TouchDog()) {
					state = "return";
					frames = 90;
					transform.forward = -transform.forward;
				}
				break;
			case "return":
				state = "sitting";
				break;
			}
		}
	}
	bool isHungry(){
		return (DatabaseReader.Instance().hunger < 720);
	}
	bool isHappy(){
		return (DatabaseReader.Instance().exp > 1440);
	}
	bool isAngry(){
		return !isHappy();
	}
	void EmotionCheck(){
		emotions.Clear ();
		if (isHungry ()) {
			emotions.Add ("hungry");
		}
		if (isHappy ()) {
			emotions.Add ("happy");
		}
		if (isAngry ()) {
			emotions.Add ("angry");
		}
	}
	void DisplayEmotion(){
		EmotionCheck ();
		SkinnedMeshRenderer mesh = transform.Find ("DogMesh").GetComponent<SkinnedMeshRenderer> ();
		int x = Random.Range (0, emotions.Count);
		switch (emotions [x]) {
		case "happy":
			mesh.SetBlendShapeWeight (0, 0);
			mesh.SetBlendShapeWeight (1, 0);
			mesh.SetBlendShapeWeight (2, 0);
			mesh.SetBlendShapeWeight (3, 0);
			mesh.SetBlendShapeWeight (4, 0);
			break;
		case "hungry":
			mesh.SetBlendShapeWeight (0, 0);
			mesh.SetBlendShapeWeight (1, 0);
			mesh.SetBlendShapeWeight (2, 100);
			mesh.SetBlendShapeWeight (3, 0);
			mesh.SetBlendShapeWeight (4, 100);
			break;
		case "angry":
			mesh.SetBlendShapeWeight (0, 0);
			mesh.SetBlendShapeWeight (1, 100);
			mesh.SetBlendShapeWeight (2, 100);
			mesh.SetBlendShapeWeight (3, 0);
			mesh.SetBlendShapeWeight (4, 100);
			break;
		}
	}

	bool TouchDog(){
		if (Input.GetMouseButton (0)) {
			Collider coll = GetComponent<Collider> ();
			Debug.Log (Camera.main.ScreenPointToRay (Input.mousePosition));
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (coll.Raycast (ray, out hit, 100)) {
				return true;
			} else {
				return false;
			}
		} else {
			return false;
		}
	}
}
