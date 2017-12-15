using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class positionScript : MonoBehaviour {

    private float originalPosition;

	// Use this for initialization
	void Start () {
        originalPosition = transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 v = transform.position;
        v.x = originalPosition;
        transform.position = v;
	}
}
