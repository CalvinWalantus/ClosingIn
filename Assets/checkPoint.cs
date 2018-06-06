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
		if(objects.transform.gameObject.tag == "Player"){
			activateCheckPoint();
		}
	}
	
	public static Vector3 returnCheckPointLocation(){
		Vector3 result = Vector3.zero;
		foreach (GameObject beacon in checkPointsList) {
			if (beacon.GetComponent<checkPoint> ().activated == true) {
				result = beacon.transform.position;
			}
		}
		return result;
	}
}
