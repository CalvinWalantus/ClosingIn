// This script should be attached to a ShiftTrigger prefab object.
// This object communicates with the worldcontroller to manually force
// a shift or disable a player's control over shifting

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftTrigger : MonoBehaviour {

	World worldcontroller;


	public bool forceShift = false; 

	// TRUE = 3D
	// FALSE = 2D
	public bool dimension = true;
	public float time = 5.0f;


	public bool toggleDimensionControl = false, toggle = false;

	public bool toggleFog = false, fog = true;

	void Start () {
		worldcontroller = FindObjectOfType<World>();
	}
	
	void OnTriggerEnter (Collider other) {
		if (other.tag.Equals("Player")) {
			if (forceShift) {
				worldcontroller.ShiftOnExternalCall(dimension, time);
			}
			if (toggleDimensionControl) {
				worldcontroller.ToggleInput(toggle);
			}
			if (toggleFog) {
				worldcontroller.ToggleFog(fog);
			}
			Destroy(this.gameObject);
		}
	}
}
