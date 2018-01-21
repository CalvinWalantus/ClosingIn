using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

	// True = 3D
	// False = 2D
	public bool dimension = false;

	public int two_shot = 1;
	public int three_shot = 1;

	public float shift_time = 5;
	public float timer = 0;

	// Event signaling a dimension change (a "shift")
	public delegate void Shift(bool dim, float time);
	public event Shift shiftEvent;

	// Event signalling a shot change
	// 1-4 = 2D shot change
	// 5-8 = 3D shot change
	public delegate void ShotChange (int tw_shot, int th_shot);
	public event ShotChange shotChangeEvent;

	void Start() {
		timer = shift_time;
		shotChangeEvent (two_shot, three_shot);
		shiftEvent (dimension, shift_time);
	}
	
	// Update is called once per frame
	void Update () {
		// Check if user has pressed shift to bring about a dimension shift
		if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && timer > shift_time) {
			dimension = !dimension;
			shiftEvent(dimension, shift_time);
			timer = 0;
		}
		timer += Time.deltaTime;

		// Check if user has pressed the 1-8 keys to bring about a shot change.
		for (int shot = 1; shot < 9; shot++) {
			if (Input.GetKeyDown((KeyCode)shot+48)) {
				if (shot < 5) {
					two_shot = shot;
				} else {
					three_shot = shot - 4;
				}
				shotChangeEvent (two_shot, three_shot);
				break;
			}
		}


	}

}
