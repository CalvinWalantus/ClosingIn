using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine; 

public class SwapableCamera : MonoBehaviour {
	World world_controller;

	MatrixBlender blender;
	Matrix4x4 pers, ortho;

	// True = 3D
	// False = 2D
	bool dimension;

	bool startFlag = true;

	// These variables are public for observation only, not to be set
	public int two_shot = 0, current_shot = 0;

	// Do we switch projection type when we shift?
	public bool blendingOrtho = true;

	// keeps track of the 5 regular virtual cameras
	public List<GameObject> shot_reference;

    private Camera cam;

    private float shift_time;

	// Use this for initialization
	void Start () {

		world_controller = FindObjectOfType<World> ();
		blender = gameObject.GetComponent<MatrixBlender> ();

		// subscribe to worldcontroller events
		if (world_controller) {
			world_controller.shiftEvent += Shift;
			world_controller.shotChangeEvent += ShotChange;
		}

		// Prepare the projection matrices based on the camera's settings.
		cam = Camera.main;
		pers = Matrix4x4.Perspective (cam.fieldOfView, cam.aspect, cam.nearClipPlane, cam.farClipPlane);
		ortho = Matrix4x4.Ortho (-cam.orthographicSize * cam.aspect, cam.orthographicSize * cam.aspect, -cam.orthographicSize, cam.orthographicSize, cam.nearClipPlane, cam.farClipPlane);
	}

	// Moves the camera within the same dimension

	void ShotChange (int tw_shot) {

		two_shot = tw_shot;

		if (dimension) {
			MoveCamera (5);
		} else
			MoveCamera (two_shot);
			blender.BlendToMatrix(ortho, shift_time);
	}


	// This function handles movement of the camera between 2D and 3D shots,
	// and the changing of projection mode
	void Shift(bool dim, float time) {

		// Assign global variables to values of event parameters
		dimension = dim;
		shift_time = time;
		GetComponent<CinemachineBrain> ().m_DefaultBlend.m_Time = time;

		// if shifting to 3d...
		if (dim) {
			// switch to the freelook camera.
			MoveCamera (5);

			// Refers to the Matrixblender script to change perspective
			if (blendingOrtho) {
				blender.BlendToMatrix(pers, time);
			}
		} 

		// else, if shifting to 2d...
		else {
			
			// if this is the very first time shift is called...
			if (startFlag) {
				// if the game starts in 2D, camera is moved by shotchange,
				// and we instantly blend to orthographic 
				if (blendingOrtho) {
					CinemachineVirtualCamera newCam = GetComponent<CinemachineBrain>().ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>();
					ortho = Matrix4x4.Ortho(-newCam.m_Lens.OrthographicSize * cam.aspect, newCam.m_Lens.OrthographicSize * cam.aspect, -newCam.m_Lens.OrthographicSize, newCam.m_Lens.OrthographicSize, cam.nearClipPlane, cam.farClipPlane);
					blender.BlendToMatrix (ortho, 0.001f);
				}

			}

			else {
				// Get the location of the freelook camera
				List<GameObject> temp = shot_reference.GetRange (0, 4);
				ICinemachineCamera locationtemp = GetComponent<CinemachineBrain> ().ActiveVirtualCamera;
				GameObject location = locationtemp.VirtualCameraGameObject;

				// iterate through the 4 2d shots, can move to the one closest to the freelook camera
				float shortest = Mathf.Infinity;
				float distance = 0;
				int index = 0;

				foreach (GameObject camera in temp) {
					distance = Vector3.Distance (location.transform.position, camera.transform.position);
					if (distance < shortest) {
						shortest = distance;
						index = temp.IndexOf (camera);
					}
				}
					
				// trigger a shotchange event, which will in turn move the camera.
				if (world_controller) {
					world_controller.ShotChangeOnExternalCall (index + 1);
				}


				// Refers to the Matrixblender script to change perspective
				if (blendingOrtho) {
					blender.BlendToMatrix (ortho, time);
				}
			} 
		}

		startFlag = false;
	}

	// Cinemachine handles all camera movement
	void MoveCamera(int shot) {

		// the freelook camera, used for 3D, statically has a priority of 15. When we want to switch to
		// 2D, we set one of the 2D cameras' priority to 20
		if (shot < 5) {
            CinemachineVirtualCamera newCam = shot_reference[shot - 1].GetComponent<CinemachineVirtualCamera>();
            newCam.Priority = 20;
            ortho = Matrix4x4.Ortho(-newCam.m_Lens.OrthographicSize * cam.aspect, newCam.m_Lens.OrthographicSize * cam.aspect, -newCam.m_Lens.OrthographicSize, newCam.m_Lens.OrthographicSize, cam.nearClipPlane, cam.farClipPlane);
        }

		// decrease the priority of the previous shot
		if (current_shot != 0 && current_shot < 5)
			shot_reference [current_shot - 1].GetComponent<CinemachineVirtualCamera> ().Priority = 10;

		current_shot = shot;

	}
}
