﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContainer : MonoBehaviour {

	public GameObject player;		// Public variable stores a reference to the player game object: Third Person Controller.
	public Vector3 offset = new Vector3(0, 0, 0);			// Private Vector3 stores the offset distance between the player and camera.

	// Use this for initialization
	void Start () 
	{
		if (player == null) 
		{
			player = GameObject.FindGameObjectWithTag("Player");
		}

		//Calculate and store the offset value by getting the distance between the player's position and camera's position.
		transform.position = player.transform.position + offset;
	}
	
	// Update is called once per frame


	// LateUpdate is called after Update each frame
	void LateUpdate () 
	{
		// Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
		transform.position = player.transform.position;
	}
}

// This script should move the transform of the camera when
// the player hits an angle-changing switch.
//