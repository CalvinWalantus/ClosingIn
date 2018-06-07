using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class collideSet : MonoBehaviour {
	public float fadeInTime = 3.0f;
	public float fadeOutTime = 3.0f;
	public Material targetMaterial;
	public GameObject targetImage;
	public GameObject [] airWalls;
	public int finished = -2;
	public int platformMoveTime = 8;
	public int platformWaitTime = 3;
	// Use this for initialization
	void Start () {
		foreach (GameObject airwall in airWalls) {
			airwall.GetComponent<BoxCollider> ().enabled = false;
		}
	}

	// Update is called once per frame
	void Update () {



		//		if (this.transform.GetComponent<Rigidbody> ().IsSleeping() == true) {
		//			foreach (GameObject airwall in airWalls) {
		//				airwall.GetComponent<MeshCollider> ().enabled = false;
		//			}
		//		}
	}

	void OnCollisionEnter(Collision objects){
		targetImage.GetComponent<Image> ().enabled = true;
		targetImage.GetComponent<Image> ().canvasRenderer.SetAlpha (0.0f);
		if (finished != 0) {
			targetImage.GetComponent<Image> ().CrossFadeAlpha (1.0f, fadeInTime, true);
			finished += 1;
		}
		foreach (GameObject airwall in airWalls) {
			airwall.GetComponent<BoxCollider> ().enabled = true;
		}
		StartCoroutine(ToggleFalse ());
	}

	void OnCollisionExit(Collision other){
		if (finished != 0) {
			targetImage.GetComponent<Image> ().CrossFadeAlpha (0.0f, fadeOutTime, true);
			finished += 1;
		}
	}

	IEnumerator ToggleFalse(){
		StopCoroutine (ToggleTrue ());
		yield return new WaitForSeconds (8);
		foreach (GameObject airwall in airWalls) {
			airwall.GetComponent<BoxCollider> ().enabled = false;
		}
		StartCoroutine(ToggleTrue ());
		Debug.Log ("1");

	}
	IEnumerator ToggleTrue(){
		StopCoroutine (ToggleFalse ());
		yield return new WaitForSeconds (3);

		foreach (GameObject airwall in airWalls) {
			airwall.GetComponent<BoxCollider> ().enabled = true;
		}
		StartCoroutine (ToggleFalse ());
		Debug.Log ("2");
	}
}
