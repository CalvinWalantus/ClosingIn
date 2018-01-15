using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class WorldController : MonoBehaviour {

	public SwapCamera mainCam;
	public ThirdPersonUserControl player;

	public bool disableShift;
	public bool startInTwoD;

	public float transitionTime = 2.0f;

	// TRUE = 2D
	// FALSE = 3D
	private bool dimension;

	GameObject[] crushables;



	// Use this for initialization
	void Awake () {
		if (mainCam == null) mainCam = GameObject.Find("Main Camera").GetComponent<SwapCamera>();

		dimension = startInTwoD;
		mainCam.Settings (dimension, transitionTime);
		player.SetDimension(dimension);

		crushables = GameObject.FindGameObjectsWithTag ("Crushable");
		foreach (GameObject block in crushables) {
			block.GetComponent<Crushable> ().StartUpSettings (transitionTime);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!disableShift && Input.GetKeyDown(KeyCode.LeftShift)) {
			dimension = !dimension;

			mainCam.Swap();
			player.SetDimension(dimension);
			foreach (GameObject block in crushables) {
				if (dimension == true) {
					block.GetComponent<Crushable>().Crush ();
				} else {
					block.GetComponent<Crushable>().Uncrush ();
				}
			}
		}
	}

	public void SetShift (bool shift = true) {
		disableShift = !shift;
	}
}
