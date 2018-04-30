using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscDebug : MonoBehaviour {

	public GameObject meshBoundTest;

	// Use this for initialization
	void Start () {
		Debug.Log("bounds: " + meshBoundTest.GetComponent<MeshRenderer>().bounds);
		Debug.Log("extents: " + meshBoundTest.GetComponent<MeshRenderer>().bounds.extents);
		Debug.Log("extent x : " + meshBoundTest.GetComponent<MeshRenderer>().bounds.extents.x);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
