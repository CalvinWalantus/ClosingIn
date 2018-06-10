using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class end : MonoBehaviour {

	public GameObject endScreen;

	void OnTriggerEnter (Collider other) {
		if (other.tag.Equals("Player")) {
			endScreen.SetActive(true);
		}
			
	}
}
