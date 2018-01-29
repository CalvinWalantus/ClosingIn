using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

	// True = 3D
	// False = 2D
	bool dimension = false;

	int two_shot;
	int three_shot;

	float shift_time = 5;
	public float timer = 0;

	// Event signaling a dimension change (a "shift")
	public delegate void Shift(bool dim, float time);
	public event Shift shiftEvent;

	// Event signalling a shot change
	// 1-4 = 2D shot change
	// 5-8 = 3D shot change
	public delegate void ShotChange (int shot);
	public event ShotChange shotChangeEvent;

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


		Event e = Event.current;
		if (e.type == EventType.KeyDown && char.IsDigit (e.character)) {
			int shot = (int)e.character;
			if (shot > 0 && shot < 9) {
				shotChangeEvent (shot);
				if (shot < 5) {
					two_shot = shot;
				} else {
					three_shot = shot - 4;
				}
			}
		}



	}

}
