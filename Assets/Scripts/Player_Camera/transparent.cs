using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transparent : MonoBehaviour {

	public GameObject player;
	private RaycastHit[] hits;
	private int layermask= 1 << 1;
	private List<Transform> first = new List<Transform> ();
	private List<Transform> second = new List<Transform> ();
	private List<Transform> reset = new List<Transform> ();
	private List<Color>tempcolor = new List<Color> ();
	private Shader original;
	// Use this for initialization
	void Start () {
		
	}
	// Update is called once per frame
	void Update () {
		float distance = Vector3.Distance (player.transform.position, Camera.main.transform.position);
		hits = Physics.CapsuleCastAll (player.transform.position + Vector3.up * 1.2f, player.transform.position + Vector3.up * 0.5f, 0.15f, (transform.position - player.transform.position).normalized, distance, layermask);
		for (int i = 0; i < hits.Length; i++) {
			RaycastHit hit = hits [i];
			if (hit.collider.tag != "Player") {
				Renderer rend = hit.transform.GetComponent<Renderer> ();
				if (rend) {
					Debug.Log (rend.bounds.size);
					hit.collider.GetComponent<Renderer> ().enabled = true;
					Debug.Log (rend.material.shader.name);
					original = rend.material.shader;
					rend.material.shader = Shader.Find ("Transparent/Diffuse");
					Color color;
					foreach (Material materials in rend.materials) {
						color = materials.color;
						color.a = 0.2f;
						materials.color = color;
						first.Add (hit.transform);
					}

					//					foreach (Material tempmaterial in rend.materials) {)
//						tempcolor.Add (tempmaterial.color);
//					}
//					for(int j =0; j<tempcolor.Count;j++){
//						Color store = tempcolor [j];
//						store.a = 0.2F;
//						tempcolor [j] = store;
//						rend.material.color = tempcolor[j];
//					}
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
				tempmaterial.shader = Shader.Find ("Standard");
			}
		}
		reset.Clear ();
		second = new List<Transform> (first);
		first.Clear ();



		Debug.DrawRay (Camera.main.transform.position, player.transform.position - Camera.main.transform.position + new Vector3 (0, 1.5f, 0), Color.red);
	}
	void OnDrawGizmos(){
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(player.transform.position+new Vector3(0,1.5f,0),1);
	}
}