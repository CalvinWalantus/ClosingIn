using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ending : MonoBehaviour {
	public GUITexture guiTextre;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter(Collision collision) {
		Debug.Log (collision.gameObject.name);
		if (collision.gameObject.name == "Caedl1") {
		}
	}

}
