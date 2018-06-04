using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class collideSet : MonoBehaviour {
	public float fadeTime;
	public Material targetMaterial;
	public GameObject targetImage;
	private float targetAlpha;
	// Use this for initialization
	void Start () {
		this.targetAlpha = this.targetImage.GetComponent<Image> ().color.a;
	}
	
	// Update is called once per frame
	void Update () {
		Color current = this.targetImage.GetComponent<Image> ().color;
		float difference = Mathf.Abs (current.a - this.targetAlpha);
		if (difference > 0.0001f) {
			current.a = Mathf.Lerp (current.a, targetAlpha, this.fadeTime * Time.deltaTime);
		}
	}

	void OnCollisionEnter(Collision objects){
		Debug.Log (objects.transform.name);
		objects.gameObject.GetComponent<MeshRenderer> ().material = targetMaterial;
		targetImage.GetComponent<Image> ().enabled = true;
	}

	void OnCollisionExit(Collision other){
		if (other.transform.tag == "Player") {
			targetImage.GetComponent<Image> ().enabled = false;
		}
	}

	public void fadeOut(){
		this.targetAlpha = 0.0f;
	}
		
	public void fadeIn(){
		this.targetAlpha = 1.0f;
	}
}
