using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

	public Transform destination;

	public World worldController;

	public bool switchDimension;
	public bool dimensionToChangeTo;
	public int twoShot, threeShot;

	public delegate void Respawn(bool dim, int two_shot, int three_shot);
	public event Respawn RespawnEvent;

	void Start () {
		worldController = FindObjectOfType<World>();
	}

	void OnTriggerEnter (Collider other) {
		if (other.CompareTag("Player")) {
			
			other.transform.position = destination.position;
			other.transform.rotation = destination.rotation;

			if (switchDimension) {
				
				RespawnEvent (dimensionToChangeTo, twoShot, threeShot);

			}
		}
	}
}
