using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transparent : MonoBehaviour {

	public GameObject player;
	private RaycastHit[] hits;
	private int layermask;
	private List<Transform> first;
	private List<Transform> second;
	private List<Transform> reset;
	private List<Color>tempcolor;
	// Use this for initialization
	void Start () {
		layermask = 1 << 1;
		first = new List<Transform> ();
		reset = new List<Transform> ();
		second = new List<Transform> ();
		tempcolor = new List<Color> ();
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
					foreach (Material tempmaterial in rend.materials) {
						tempcolor.Add (tempmaterial.color);
					}
					for(int j =0; j<tempcolor.Count;j++){
						Color store = tempcolor [j];
						store.a = 0.2F;
						tempcolor [j] = store;
						rend.material.color = tempcolor[j];
						first.Add (hit.transform);
					}
					tempcolor.Clear ();
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
			foreach (Material tempmaterial in clear.materials) {
				Color tempc = tempmaterial.color;
				tempc.a = 1.0f;
				tempmaterial.color = tempc;
			}
		}
		reset.Clear ();
		second = new List<Transform> (first);
		first.Clear ();



		Debug.DrawRay (Camera.main.transform.position, player.transform.position - Camera.main.transform.position+ new Vector3(0,1.5f,0), Color.red);
	}
	void OnDrawGizmos(){
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(player.transform.position+new Vector3(0,1.5f,0),1);
	}
}