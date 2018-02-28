using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableComponentsOnStartup : MonoBehaviour {

	public bool gameObject__, collider__, meshRenderer__;

	void Start () {
		if (gameObject__) {
			gameObject.SetActive(false);
			return;
		}

		if (collider__) {
			if (gameObject.GetComponent<BoxCollider>() != null) {

				// TODO: make this work for all collider types.
				GetComponent<BoxCollider>().enabled = false;
			}
		}

		if (meshRenderer__) {
			GetComponent<MeshRenderer>().enabled = false;
		}
	}
}
