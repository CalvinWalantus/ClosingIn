using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

	public Transform destination;

	void OnTriggerEnter (Collider other) {
		if (other.CompareTag("Player")) {
			other.transform.position = destination.position;
			other.transform.rotation = destination.rotation;
		}
	}
}
