using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapableCamera : MonoBehaviour {

	public World world_controller;

	// Use this for initialization
	void Start () {
		world_controller.shiftEvent += Shift;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Shift(bool dimension, float time) {
		Debug.Log(dimension + " " + time);
	}
}
