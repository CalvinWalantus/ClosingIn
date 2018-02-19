using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Should be attached to Wall of Fog gameobject, child of Main Camera
// This script increases the opacity of the wall of fog behind the player
// when in 2D

[RequireComponent(typeof(Material))]
public class WallofFog : MonoBehaviour {

	World worldController;

	public float opacity;
	Material fog, invisible;
	Color startColor;

	bool startFlag = true;

	// Use this for initialization
	void Awake () {
		worldController = FindObjectOfType<World> ();
		worldController.shiftEvent += HandleShift;

		fog = gameObject.GetComponent<Renderer>().material;
		startColor = fog.color;
	}

	void HandleShift (bool dim, float time) {
		if (!startFlag) {
			if (dim) {
				StartCoroutine (LerpColor (startColor, Color.clear, dim, time));
			} else {
				StartCoroutine (LerpColor (Color.clear, startColor, dim, time));
			}
		} else {
			if (dim) {
				fog.color = Color.clear;
			} else {
				fog.color = startColor;
				GetComponent<MeshRenderer> ().enabled = true;
			}
			startFlag = false;
		}
	}
	
	private IEnumerator LerpColor(Color src, Color dest, bool disappearing, float duration)
	{
		float startTime = Time.time;

		if (!disappearing) {
			GetComponent<MeshRenderer> ().enabled = true;
		}

		while (Time.time - startTime < duration)
		{
			fog.color = Color.Lerp (src, dest, (Time.time - startTime) / duration);
			yield return 1;
		}

		if (disappearing) {
			GetComponent<MeshRenderer> ().enabled = false;
		}
	}
}
