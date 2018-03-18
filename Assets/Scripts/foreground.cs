﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class foreground : MonoBehaviour {
	public GameObject player;
	public List<Collider> disables = new List<Collider>();
	public List<Collider> temp = new List<Collider> ();
	public Collider[] hits;
	World world_controller;
	public int layermask;
	// Use this for initialization
	void Start () {
		world_controller = FindObjectOfType<World> ();
		layermask = 1 << 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (world_controller.dimension == false) {
			findobjects ();
		} else {
			foreach (Collider i in disables.ToList()) {
				i.GetComponent<Renderer> ().enabled = true;
			}
		}
	}

	void findobjects(){
		temp.Clear ();
		float x = (player.transform.position.x + Camera.main.transform.position.x)/2;
		float y = (player.transform.position.y + Camera.main.transform.position.y)/2;
		float z = (player.transform.position.z + Camera.main.transform.position.z)/2;
		Vector3 pos = new Vector3 (x, y, z);
		float size = Vector3.Distance (player.transform.position, Camera.main.transform.position);
		size = size * 0.9f;
		hits = Physics.OverlapBox (pos, new Vector3 (20f/2, 8.3f/2, size/2),Quaternion.identity,layermask);
		//debugdraw (hits);
		temp = hits.ToList();
		foreach (Collider q in disables.ToList()) {
			if (!temp.Contains (q)) {
				q.GetComponent<Renderer> ().enabled = true;
				disables.Remove (q);
				//Debug.Log (q.gameObject.name);
			}
		}
		foreach(Collider i in temp){
			if (world_controller.two_shot == 3) {
				if (i.gameObject.transform.position.z - i.gameObject.transform.lossyScale.z / 2 > player.transform.position.z && !disables.Contains(i)) {
					if (i.GetComponent<Renderer> ()) {
						i.GetComponent<Renderer> ().enabled = false;
						disables.Add (i);
						Debug.Log (i.gameObject.name);
					}
				}
			}
			if (world_controller.two_shot == 1) {
				if (i.gameObject.transform.position.z + i.gameObject.transform.lossyScale.z / 2 < player.transform.position.z && !disables.Contains(i)) {
					if (i.GetComponent<Renderer> ()) {
						i.GetComponent<Renderer> ().enabled = false;
						disables.Add (i);
					}
				}
			}
			if (world_controller.two_shot == 4) {
				if (i.gameObject.transform.position.x + i.gameObject.transform.lossyScale.z / 2 < player.transform.position.x && !disables.Contains(i)) {
					if (i.GetComponent<Renderer> ()) {
						i.GetComponent<Renderer> ().enabled = false;
						disables.Add (i);
					}
				}
			}
			if (world_controller.two_shot == 2) {
				if (i.gameObject.transform.position.x - i.gameObject.transform.lossyScale.x / 2 > player.transform.position.x && !disables.Contains(i)) {
					if (i.GetComponent<Renderer> ()) {
						i.GetComponent<Renderer> ().enabled = false;
						disables.Add (i);
					}
				}
			}


		}

	}
	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		float x = (player.transform.position.x + Camera.main.transform.position.x)/2;
		float y = (player.transform.position.y + Camera.main.transform.position.y)/2;
		float z = (player.transform.position.z + Camera.main.transform.position.z)/2;
		Vector3 pos = new Vector3 (x, y, z);
		float size = Vector3.Distance (player.transform.position, Camera.main.transform.position);
		Gizmos.DrawWireCube(pos,new Vector3(20f,8.3f,size));
	}
	void debugdraw(Collider[] hits){
		foreach (Collider i in hits) {
			if (i.gameObject.GetComponent<Renderer> ()) {
				i.GetComponent<Renderer> ().material.color = Color.red;
			}
		}
	}
}
