using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class visible : MonoBehaviour {
	public List<Renderer> visibleRenderers = new List<Renderer>();
	public List<Renderer> temp = new List<Renderer>();
	public GameObject player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Renderer[] sceneRenderers = FindObjectsOfType<Renderer> ();

		// Store only visible renderers
		visibleRenderers.Clear ();
		for (int i = 0; i < sceneRenderers.Length; i++) {
			if (sceneRenderers [i].isVisible)
				visibleRenderers.Add (sceneRenderers [i]);
		}
		foreach (Renderer i in visibleRenderers) {
			if (i.gameObject.transform.position.z < player.transform.position.z && i.gameObject.transform.position.z < Camera.main.transform.position.z) {
				i.GetComponent<Renderer> ().enabled = false;
				temp.Add (i);
			}
		}
	}
//	bool leftorright(Transform play, Vector3 target){
//		//float temp = play.InverseTransformPoint (target);
//		//if (temp != 0) {
//		//	return true;
//		//} else {
//		//	return false;
//		//}
//		return true;
//	}
}
