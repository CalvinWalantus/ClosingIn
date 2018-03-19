using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phonesound : MonoBehaviour {
	public AudioClip clip;
	public AudioSource source;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void onCollisionEnter(Collision temp){
		if (temp.transform.tag == "Player") {
			source.Play ();
		}
	}
}
