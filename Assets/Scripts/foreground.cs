using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class foreground : MonoBehaviour {
	public GameObject player;
	private List<GameObject> disables = new List<GameObject>();
	public List<Collider> temp = new List<Collider> ();
	World world_controller;
	// Use this for initialization
	void Start () {
		world_controller = FindObjectOfType<World> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		if (world_controller.dimension == false) {
			findobjects ();
		}
	}

	void findobjects(){
		float x = (player.transform.position.x + Camera.main.transform.position.x)/2;
		float y = (player.transform.position.y + Camera.main.transform.position.y)/2;
		float z = (player.transform.position.z + Camera.main.transform.position.z)/2;
		Vector3 pos = new Vector3 (x, y, z);
		float size = Vector3.Distance (player.transform.position, Camera.main.transform.position);
		size = size * 0.9f;
		pos = new Vector3 (pos.x + 10, pos.y, pos.z);
		Collider[] hits = Physics.OverlapBox (pos, new Vector3 (20f/2, 8.3f/2, size/3));
		Debug.Log (hits.Length);
		//List<Collider> temp = hits.ToList ();
		foreach(GameObject i in disables){
			if(temp.Contains(i.GetComponent<Collider>()))
				i.GetComponent<Renderer> ().enabled = true;
		}
		foreach (Collider i in hits) {
			if (i.gameObject.GetComponent<Renderer> ()) {
				i.gameObject.GetComponent<Renderer> ().enabled = false;
				disables.Add (i.gameObject);
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
		size = size * 0.9f;

		Gizmos.DrawCube(pos,new Vector3(20f,8.3f,size));
		Debug.Log(Camera.current.pixelRect);
	}

}
