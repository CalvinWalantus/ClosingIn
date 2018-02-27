using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testings : MonoBehaviour {

	public GameObject player;
	private RaycastHit[] hits;
	private int layermask;
	private List<Transform> first;
	private List<Transform> second;
	private List<Transform> reset;
	// Use this for initialization
	void Start () {
		layermask = 1 << 1;
		first = new List<Transform> ();
		reset = new List<Transform> ();
		second = new List<Transform> ();
	}
	// Update is called once per frame
	void Update () {
		float distance = Vector3.Distance (player.transform.position, Camera.main.transform.position);
		hits = Physics.SphereCastAll (Camera.main.transform.position, 0.5f,(player.transform.position - Camera.main.transform.position)+ new Vector3(0,1,0), distance, layermask);
		for (int i = 0; i < hits.Length; i++) {
			RaycastHit hit = hits [i];
			if (hit.collider.tag != "Player") {
				Renderer rend = hit.transform.GetComponent<Renderer> ();
				if (rend) {
					hit.collider.GetComponent<Renderer> ().enabled = true;
					rend.material.shader = Shader.Find ("Transparent/Diffuse");
					Color tempColor = rend.material.color;
					tempColor.a = 0.2F;
					rend.material.color = tempColor;
					first.Add (hit.transform);
				}
			}
		}


		for (int n = 0; n < second.Count; n++) {
			if (!first.Contains (second [n])) {
				reset.Add (second [n]);
			}
		}
		foreach (Transform temp in reset) {
			Renderer clear = temp.transform.GetComponent<Renderer> ();
			Color tempc = clear.material.color;
			tempc.a = 1.0f;
			clear.material.color = tempc;
		}
		reset.Clear ();
		second = new List<Transform> (first);
		first.Clear ();



		Debug.DrawRay (Camera.main.transform.position, player.transform.position - Camera.main.transform.position+ new Vector3(0,1,0), Color.red);
	}
	void OnDrawGizmos(){
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(player.transform.position+new Vector3(0,1.0f,0),1);
	}
}
