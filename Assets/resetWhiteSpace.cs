using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetWhiteSpace : MonoBehaviour {
	Vector3 result;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space")) {
			result = checkPoint.returnCheckPointLocation ();
			if (result != Vector3.zero) {
				this.transform.position = result + new Vector3 (0, 0, 0);
			}
		}
	}
}
