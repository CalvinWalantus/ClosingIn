using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FindLargeColliders : MonoBehaviour {
	
	public bool go = false;

	// Update is called once per frame
	void Update () {
		if (go) {
			foreach (GameObject thing in GameObject.FindObjectsOfType<GameObject>()) {
				if (thing.GetComponent<MeshCollider>() != null) {
					if (thing.GetComponent<MeshRenderer>().materials.Length > 1) {
						thing.AddComponent<FindThis>();
					}
				}
			}
			go = false;
		}
	}
}
