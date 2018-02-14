using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine; 

public class SwapableCamera : MonoBehaviour {

	public bool switchingOrtho = true;

	public World world_controller;

	Dictionary<int, GameObject> shot_reference;

	// True = 3D
	// False = 2D
	bool dimension;

	public int two_shot = 0, three_shot = 0, current_shot = 0;

	public List<GameObject> shots;

	// Use this for initialization
	void Start () {
		world_controller.shiftEvent += Shift;
		world_controller.shotChangeEvent += ShotChange;

		shot_reference = new Dictionary<int, GameObject> ();
		int i = 1;
		foreach (GameObject shot in shots) {
			shot_reference.Add (i, shot);
			i++;
		}

	}

	void ShotChange (int tw_shot, int th_shot) {

		if (dimension)
			MoveCamera (th_shot + 4);
		else
			MoveCamera (tw_shot);

		two_shot = tw_shot;
		three_shot = th_shot;
	}

	void Shift(bool dim, float time) {
		
		if (dim) {
			MoveCamera (three_shot + 4);
			if (switchingOrtho) {
				Camera.main.orthographic = false;
			}
		} else {
			MoveCamera (two_shot);
			if (switchingOrtho) {
				Camera.main.orthographic = true;
			}
		}
		dimension = dim;
		GetComponent<CinemachineBrain> ().m_DefaultBlend.m_Time = time;
	}

	void MoveCamera(int shot) {
		shot_reference [shot].GetComponent<CinemachineVirtualCamera> ().Priority = 20;
		if (current_shot != 0)
			shot_reference [current_shot].GetComponent<CinemachineVirtualCamera> ().Priority = 10;
		current_shot = shot;

	}
}
