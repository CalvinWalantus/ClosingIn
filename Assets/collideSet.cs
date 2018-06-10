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
	public float platformMoveTime;
	public float platformWaitTime;
	// Use this for initialization
	void Start () {
		if (airWalls.Length > 0) {
			foreach (GameObject airwall in airWalls) {
			
				airwall.GetComponent<BoxCollider> ().enabled = false;
			}
		}
		platformMoveTime = GetComponentInParent<MovingPlatform>().moveTime;
		platformWaitTime = GetComponentInParent<MovingPlatform>().waitsecond;
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
		targetImage.GetComponent<RawImage> ().enabled = true;
		targetImage.GetComponent<RawImage> ().canvasRenderer.SetAlpha (0.0f);
		if (finished != 0) {
			targetImage.GetComponent<RawImage> ().CrossFadeAlpha (1.0f, fadeInTime, true);
			finished += 1;
		}
		foreach (GameObject airwall in airWalls) {
			airwall.GetComponent<BoxCollider> ().enabled = true;
		}
		StartCoroutine(ToggleFalse ());
	}

	void OnCollisionExit(Collision other){
		if (finished != 0) {
			targetImage.GetComponent<RawImage> ().CrossFadeAlpha (0.0f, fadeOutTime, true);
			finished += 1;
		}
	}

	IEnumerator ToggleFalse(){
		StopCoroutine (ToggleTrue ());
		yield return new WaitForSeconds (platformMoveTime);
		foreach (GameObject airwall in airWalls) {
			airwall.GetComponent<BoxCollider> ().enabled = false;
		}
		StartCoroutine(ToggleTrue ());
		Debug.Log ("1");

	}
	IEnumerator ToggleTrue(){
		StopCoroutine (ToggleFalse ());
		yield return new WaitForSeconds (platformWaitTime);

		foreach (GameObject airwall in airWalls) {
			airwall.GetComponent<BoxCollider> ().enabled = true;
		}
		StartCoroutine (ToggleFalse ());
		Debug.Log ("2");
	}
}
