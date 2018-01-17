using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {

	public GameObject respawn_location;
	public string player_tag = "Player";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider other) {
		if (other.tag.Equals(player_tag)) {
			other.transform.position = respawn_location.transform.position;
			other.transform.rotation = respawn_location.transform.rotation;
		}
	}
}
