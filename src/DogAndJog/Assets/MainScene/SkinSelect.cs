using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSelect : MonoBehaviour {

	public Texture[] textures;

	// Use this for initialization
	void Start () {
		GetComponent<Renderer> ().materials [0].mainTexture = textures [DatabaseReader.Instance().currentSkin];
	}
}
