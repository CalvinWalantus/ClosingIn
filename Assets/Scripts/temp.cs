using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class temp : MonoBehaviour {
	public Transform target;
	public float Transparentnmber = 0.2f;
	public float fadetime = 1.0f;
	private int targetlayer = 1<<1;
	public Renderer[] renderers;
	public List<Renderer> reset = new List<Renderer>();
	public Dictionary<Renderer,temptransparent> transparentdictionary= new Dictionary<Renderer, temptransparent>();

	public class temptransparent{ 
		public Material[] materials = null;  
		public Material[] sharedmaterials = null;  
		public float fadetime = 0;  
		public bool istransparent = true;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (target == null)
			return;
		maketransparent ();
		checkobject ();
		resettransparent ();
		
	}
	public void checkobject(){
		RaycastHit[] Hits = null;
		Vector3 targetposition = target.position + new Vector3 (0, 3.0f, 0);
		Vector3 direction = (targetposition - Camera.main.transform.position).normalized;
		float distance = Vector3.Distance (Camera.main.transform.position, targetposition);
		Ray raycast = new Ray (Camera.main.transform.position, targetposition);
		Hits = Physics.RaycastAll (raycast, distance, targetlayer);
		Debug.DrawLine (Camera.main.transform.position, targetposition, Color.black);
		foreach (RaycastHit hit in Hits) {

			renderers = hit.collider.GetComponentsInParent<Renderer>();  
			foreach (Renderer r in renderers) {  
				addtransparent (r); 
			}  
		}
			
	}

	public void maketransparent(){
		var var = transparentdictionary.GetEnumerator ();
		while (var.MoveNext ()) {
			temptransparent temp = var.Current.Value;
			temp.istransparent = false;
			foreach (Material material in temp.materials) {
				Color color = material.GetColor ("_Color");
				temp.fadetime += Time.deltaTime;
				float time = temp.fadetime / fadetime;
				color.a = Mathf.Lerp (1, Transparentnmber, time);
				material.SetColor ("_Color", color);
			}
		}
	}


	public void resettransparent(){
		reset.Clear ();
		var var = transparentdictionary.GetEnumerator ();
		while (var.MoveNext ()) {
			if (var.Current.Value.istransparent == false) {
				var.Current.Key.materials = var.Current.Value.sharedmaterials;
				reset.Add (var.Current.Key);
			}
		}
		foreach (var v in reset) {
			transparentdictionary.Remove (v);
		}

	}

	public void addtransparent(Renderer renderder){
		temptransparent temp = new temptransparent();  
			//transparentDic.Add(renderer, param);
		transparentdictionary.Add(renderder,temp);
		temp.sharedmaterials = renderder.sharedMaterials;  
		temp.materials = renderder.materials;  
		foreach(Material q in temp.materials)  
		{  
			q.shader = Shader.Find("ApcShader/OcclusionTransparent");  
		}  
		temp.istransparent = true;
	}
}
