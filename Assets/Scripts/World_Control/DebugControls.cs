using System;
using UnityEngine;

public class DebugControls : MonoBehaviour 
{
	public Transform[] spawnLocations;
	public GameObject player;

	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update () {

		int oneKeyCode = (int)KeyCode.Alpha1;

		for (int i = 0; i < 10; i++) {
			if (Input.GetKeyDown((KeyCode)(oneKeyCode + i))) {
				player.transform.parent = null;
				player.transform.position = spawnLocations[i].position;
			}
		}
	}
}