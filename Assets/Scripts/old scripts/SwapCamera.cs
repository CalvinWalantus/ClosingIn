using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Camera))]
public class SwapCamera : MonoBehaviour {

	Transform threeDCamTrans;
	Transform twoDCamTrans;
	public WorldController world_controller;

	public int two_d_start_pos;
	public int three_d_start_pos;


	public Transform position_one;
	public Transform position_two;
	public Transform position_three;
	public Transform position_four;
	public Transform two_position_one;
	public Transform two_position_two;
	public Transform two_position_three;
	public Transform two_position_four;
	Transform destination;

	float positionSpeed;
	float rotationSpeed;

	// DEPRECATED - we aren't really using twoDOrtho anymore, leave false
	bool twoDOrtho = false;

	// These vars are set by worldcontroller
	bool startInTwoD;
	float transitionTime;


	int dimension;


	bool transitioning = false;
	bool switching_dimensions = false;

	Camera mainCam;

	void Start () {
		mainCam = GetComponent<Camera>();

		//ChangePosition (two_d_start_pos, 2);
		twoDCamTrans = two_position_one;
		ChangePosition (three_d_start_pos, 3, false);

		if (startInTwoD) {
			ChangeDimensionSettings(2, true);
		}
		else {
			ChangeDimensionSettings(3, true);
		}


	}
	

	void Update () {
		if (transitioning) {
			float positionStep = positionSpeed * Time.deltaTime;
			float rotationStep = rotationSpeed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, destination.position, positionStep);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, destination.rotation, rotationStep);
			if (transform.position == destination.position && transform.rotation == destination.rotation) {
				transitioning = false;
				if (switching_dimensions) {
					if (dimension == 2) {
						ChangeDimensionSettings(3);
					}
					else {
						ChangeDimensionSettings(2);
					}
					switching_dimensions = false;
				}
				else {
					ChangeDimensionSettings(dimension);
				}
			}
		}
		else {
			if (dimension == 2) TwoDFollowBehavior();
			else                ThreeDFollowBehavior();
		}
	}

	public void Swap() {
		if (!transitioning) {
			transitioning = true;
			switching_dimensions = true;
		}
	}

	void ChangeDimensionSettings ( int dim, bool hardPositionSet = false ) {
		if (dim == 2) {
			dimension = 2;
			destination = threeDCamTrans.transform;
			if (twoDOrtho) mainCam.orthographic = true;
			if (hardPositionSet) {
				transform.position = twoDCamTrans.position;
				transform.rotation = twoDCamTrans.rotation;
			}
		}
		else {
			dimension = 3;
			destination = twoDCamTrans.transform;
			if (twoDOrtho) mainCam.orthographic = false;
			if (hardPositionSet) {
				transform.position = threeDCamTrans.position;
				transform.rotation = threeDCamTrans.rotation;
			}
		}

		SetSpeeds ();
	}

	void TwoDFollowBehavior () {

	}

	void ThreeDFollowBehavior () {
		
	}

	// INCOMPLETE BUT FUNCTIONAL FOR 3D
	// used to change the position of the camera for a specific
	// dimension.
	public void ChangePosition (int position, int dimensionChanging, bool moveImmediately = true) {
		Transform new_position;
		if (dimensionChanging == 3) {
			if (position == 4) {
				new_position = position_four;
			} else if (position == 3) {
				new_position = position_three;
			} else if (position == 2) {
				new_position = position_two;
			} else if (position == 1) {
				new_position = position_one;
			} else {
				new_position = null;
			}
			threeDCamTrans = new_position;
		}
		else {
			if (position == 4) {
				new_position = two_position_four;
			} else if (position == 3) {
				new_position = two_position_three;
			} else if (position == 2) {
				new_position = two_position_two;
			} else if (position == 1) {
				new_position = two_position_one;
			} else {
				new_position = null;
			}
			twoDCamTrans = new_position;
		}

		// TEMPORARY FIX
		if (moveImmediately) {
			transitioning = true;
			destination = new_position;
			SetSpeeds ();
			Debug.Log (new_position);
		}
		//


	}

	void SetSpeeds () {
		
		float positionDistance = Vector3.Distance (transform.position, destination.position);
		float rotationDistance = Quaternion.Angle (transform.rotation, destination.rotation);

		positionSpeed = positionDistance / transitionTime;
		rotationSpeed = rotationDistance / transitionTime;

	}

	public void Settings (bool twoD, float time) {
		startInTwoD = twoD;
		transitionTime = time;
	}
}
