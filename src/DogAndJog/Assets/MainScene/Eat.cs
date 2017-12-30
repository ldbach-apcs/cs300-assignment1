using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eat : MonoBehaviour {

	public GameObject Dog;
	public int index;

	// Use this for initialization
	void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {
	}

	public void Eating(){
		//if (DogGetComponent<Animation> () ["Eating"].normalizedTime < 0.9)
		Dog.GetComponent<Animation>().Stop();
		Dog.GetComponent<Animation>().Play ("Eating");
		Dog.GetComponent<Animation> ().Play ();
	}
}
