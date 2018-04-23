using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iphonesound : MonoBehaviour {
	public AudioSource audioplay;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter(Collision collision)
	{
		Debug.Log (collision.gameObject.name);
		if (collision.gameObject.name == "Caedl1") {
			audioplay.Play ();
			audioplay.loop = true;
			audioplay.spatialBlend = 1f;
		}
	}
}
