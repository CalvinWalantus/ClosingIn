using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetWhiteSpace : MonoBehaviour {
	Vector3 result;
	public Vector3 destination;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space")) {
			//this.transform.position = destination;
			//Debug.DrawRay (this.transform.position, destination, Color.green);
			result = checkPoint.returnCheckPointLocation ();
			if (result != Vector3.zero) {
				this.transform.position = result + new Vector3 (0, 0, 0);
			}
		}
	}
	void OnCollisionEnter(Collision objects){
		Debug.Log (objects.transform.name);
	}
}
