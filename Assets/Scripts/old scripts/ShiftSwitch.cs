using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftSwitch : MonoBehaviour {

	public WorldController world_controller;

	public string player_tag = "Player";

	// Use this for initialization
	void Start () {
		if (!world_controller) world_controller = GameObject.FindGameObjectWithTag("WorldController").GetComponent<WorldController>();
	}
	
	void OnTriggerEnter (Collider other) {
		if (other.tag.Equals(player_tag)) {
			world_controller.SetShift(true);
			Debug.Log("ShiftSwitch");
		}
	}
}
