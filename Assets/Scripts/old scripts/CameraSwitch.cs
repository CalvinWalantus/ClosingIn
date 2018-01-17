using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour {

	public SwapCamera cam;
	public int three_d_change, two_d_change;

	public bool destroy_switch = true;

	AudioSource aud;

	// Use this for initialization
	void Start () {
		if (!cam) cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SwapCamera>();
		aud = this.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider other) {
		if (other.tag.Equals("Player")) {
			cam.ChangePosition(three_d_change, 3);
			cam.ChangePosition (two_d_change, 2, false);
			aud.Play ();
			Destroy (this.gameObject);
		}
	}
}
