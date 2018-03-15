using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foreground : MonoBehaviour {
	public GameObject player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void findobjects(){
		

	}
	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		float x = (player.transform.position.x + Camera.main.transform.position.x)/2;
		float y = (player.transform.position.y + Camera.main.transform.position.y)/2;
		float z = (player.transform.position.z + Camera.main.transform.position.z)/2;
		Vector3 pos = new Vector3 (x, y, z);
		float size = Vector3.Distance (player.transform.position, Camera.main.transform.position);
		Gizmos.DrawCube(pos,new Vector3(10,10,10));
	}

}
