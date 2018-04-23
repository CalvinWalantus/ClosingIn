using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fadeout : MonoBehaviour {
	public Image whitescreen;
	// Use this for initialization
	void Start () {
		whitescreen.canvasRenderer.SetAlpha (0.0f);
		fade ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void fade(){
		whitescreen.CrossFadeAlpha (0.0f, 3.0f, true);
	}
}
