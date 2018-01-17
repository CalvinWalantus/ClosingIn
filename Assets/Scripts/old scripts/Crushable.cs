using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crushable : MonoBehaviour {

	Vector3 startTransform; 
	public Transform playerTransform;

	float transitionTime;
	float speed;

	bool crushing = false, uncrushing = false;

	Vector3 crushedLocation;

	// Use this for initialization
	void Awake () {
		startTransform = new Vector3(transform.position.x, transform.position.y, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		if (crushing) {
			
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, crushedLocation, step);
			if (transform.position == crushedLocation) {
				crushing = false;
			}
		}
		if (uncrushing) {

			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, startTransform, step);
			if (transform.position == startTransform) {
				uncrushing = false;
			}
		}
	}

	public void Crush () {
		crushing = true;
		crushedLocation = new Vector3 (playerTransform.position.x, transform.position.y, transform.position.z);
	}

	public void Uncrush () {
		uncrushing = true;
	}

	public void StartUpSettings (float transitionT) {
		transitionTime = transitionT;
		speed = Vector3.Distance (startTransform, playerTransform.position) / transitionTime * 2;
	}
}
