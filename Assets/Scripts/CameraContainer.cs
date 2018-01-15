using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContainer : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
		if (player == null) {
			player = GameObject.FindGameObjectWithTag("Player");
		}
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = player.transform.position;
	}
}

// This script should move the transform of the camera when
// the player hits an angle-changing switch.
//