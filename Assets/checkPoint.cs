using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPoint : MonoBehaviour {
	public bool activated = false;
	public static GameObject[] checkPointsList;
	// Use this for initialization
	void Start () {
		checkPointsList = GameObject.FindGameObjectsWithTag ("checkPoints");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void activateCheckPoint(){
		foreach (GameObject beacon in checkPointsList) {
			beacon.GetComponent<checkPoint> ().activated = false;
		}
		this.GetComponent<checkPoint> ().activated = true;
	}

	void OnCollisionEnter(Collision objects){
		if(o
}
