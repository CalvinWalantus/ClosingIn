using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Cinemachine; 

public class SwapableCamera : MonoBehaviour {
	World world_controller;

	Dictionary<int, GameObject> shot_reference;

	MatrixBlender blender;
	Matrix4x4 pers, ortho;

	// True = 3D
	// False = 2D
	bool dimension;

	// These variables are public for observation only, not
	public int two_shot = 0, three_shot = 0, current_shot = 0;

	// Do we switch projection type when we shift?
	public bool blendingOrtho = true;

	public List<GameObject> shots;

	// Use this for initialization
	void Start () {
		
		world_controller = FindObjectOfType<World> ();
		blender = gameObject.GetComponent<MatrixBlender> ();

		world_controller.shiftEvent += Shift;
		world_controller.shotChangeEvent += ShotChange;

		shot_reference = new Dictionary<int, GameObject> ();
		int i = 1;
		foreach (GameObject shot in shots) {
			shot_reference.Add (i, shot);
			i++;
		}

		// Prepare the projection matrices based on the camera's data.
		Camera cam = Camera.main;
		pers = Matrix4x4.Perspective (cam.fieldOfView, cam.aspect, cam.nearClipPlane, cam.farClipPlane);
		ortho = Matrix4x4.Ortho (-cam.orthographicSize * cam.aspect, cam.orthographicSize * cam.aspect, -cam.orthographicSize, cam.orthographicSize, cam.nearClipPlane, cam.farClipPlane);
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

			// Refers to the Matrixblender script to change perspective
			if (blendingOrtho) {
				blender.BlendToMatrix(pers, time);
			}
		} else {
			
			MoveCamera (two_shot);

			// Refers to the Matrixblender script to change perspective
			if (blendingOrtho) {
				blender.BlendToMatrix(ortho, time);
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
