using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class collideSet : MonoBehaviour {
	public float fadeTime;
	public Material targetMaterial;
	public GameObject targetImage;
	private float targetAlpha;
	public GameObject [] airWalls;
	// Use this for initialization
	void Start () {
		this.targetAlpha = this.targetImage.GetComponent<Image> ().color.a;
		foreach (GameObject airwall in airWalls) {
			airwall.GetComponent<MeshCollider> ().enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		Color current = this.targetImage.GetComponent<Image> ().color;
		float difference = Mathf.Abs (current.a - this.targetAlpha);
		if (difference > 0.0001f) {
			current.a = Mathf.Lerp (current.a, targetAlpha, this.fadeTime * Time.deltaTime);
		}

		if (this.transform.GetComponent<Rigidbody> ().IsSleeping() == true) {
			foreach (GameObject airwall in airWalls) {
				airwall.GetComponent<MeshCollider> ().enabled = false;
			}
		}
	}

	void OnCollisionEnter(Collision objects){
		Debug.Log ("hyi");
		fadeIn ();
		Debug.Log (objects.transform.name);
		objects.gameObject.GetComponent<MeshRenderer> ().material = targetMaterial;
		targetImage.GetComponent<Image> ().enabled = true;
		foreach (GameObject airwall in airWalls) {
			airwall.GetComponent<MeshCollider> ().enabled = true;
		}
	}

	void OnCollisionExit(Collision other){
		if (targetImage.GetComponent<Image> ().color.a == 0.0) {
			targetImage.GetComponent<Image> ().enabled = false;
		}
		fadeOut ();
	}

	public void fadeOut(){
		this.targetAlpha = 0.0f;
	}
		
	public void fadeIn(){
		this.targetAlpha = 1.0f;
	}
}
