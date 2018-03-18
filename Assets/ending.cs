using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ending : MonoBehaviour {
	public Texture2D fadeTexture;

	[Range(0.1f,1f)]
	public float fadespeed;
	public int drawDepth = -1000;

	private float alpha = 0.0f;
	private float fadeDir = -1f;
	private bool startfade;
	// Use this for initialization
	void Start () {
		startfade = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter(Collision collision) {
		Debug.Log (collision.gameObject.name);
		if (collision.gameObject.name == "Caedl1") {
			startfade = true;
		}
	}
	void OnGUI() {
		if (startfade) {
			alpha -= fadeDir * fadespeed * Time.deltaTime;
			alpha = Mathf.Clamp01 (alpha);

			Color newColor = GUI.color; 
			newColor.a = alpha;

			GUI.color = newColor;

			GUI.depth = drawDepth;

			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), fadeTexture);
			string words = "Coming Up on spring quarter";
			words = GUI.TextField (new Rect (100, 100, 300, 300), words, 50);
		}
	}

}
