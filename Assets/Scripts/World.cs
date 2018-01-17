using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

	// True = 3D
	// False = 2D
	bool dimension = false; 
	float shift_time = 5;
	public float timer = 0;

	public delegate void Shift(bool dim, float time);
	public event Shift shiftEvent;

	void Start() {
		timer = shift_time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.LeftShift) && timer > shift_time) {
			shiftEvent(dimension, shift_time);
			timer = 0;
		}
		timer += Time.deltaTime;
	}

}
