using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezePosition : MonoBehaviour {

	public World world_controller;

	bool dimension;
	int two_shot;
	// Use this for initialization
	void Start () {
		world_controller = FindObjectOfType<World> ();
		world_controller.shotChangeEvent += ShotChange;
		world_controller.shiftEvent += Shift;
	}

	bool startflag = true;

	void Shift (bool dim, float time) {
		if (startflag) {
			startflag = false;
			return;
		}
		dimension = dim;
		if (!dimension) {
			if (two_shot % 2 == 1) {
				GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezePositionZ;
			} else {
				GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezePositionX;
			}
		} else {
			GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
		}
		GetComponent<Rigidbody> ().freezeRotation = true;
	}
	
	void ShotChange (int tw_shot, int th_shot) {
		two_shot = tw_shot;
		if (!dimension)
			Shift (dimension, 0);
	}
}
