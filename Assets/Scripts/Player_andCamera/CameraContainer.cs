using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContainer : MonoBehaviour {

	public GameObject player;		

	// Private Vector3 stores the offset distance between the player and camera.
	public Vector3 offset = new Vector3(0, 0, 0);			

	// Use this for initialization
	void Start () 
	{
		if (player == null) 
		{
			player = GameObject.FindGameObjectWithTag("Player");
		}

		// Calculate and store the offset value by getting the distance between 
		// the player's position and camera's position.
		transform.position = player.transform.position + offset;
	}



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