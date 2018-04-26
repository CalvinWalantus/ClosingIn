using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System.Linq;
//
//public class temp : MonoBehaviour {
//	public Transform target;
//	public float Transparentnmber = 0.2f;
//	public float fadetime = 1.0f;
//	private int targetlayer = 1<<1;
//	public Renderer[] renderers;
//	public List<Renderer> reset = new List<Renderer>();
//	public Dictionary<Renderer,temptransparent> transparentdictionary= new Dictionary<Renderer, temptransparent>();
//
//	public class temptransparent{ 
//		public Material[] materials = null;  
//		public Material[] sharedmaterials = null;  
//		public float fadetime = 0;  
//		public bool istransparent = true;
//	}
//	// Use this for initialization
//	void Start () {
//		
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		if (target == null)
//			return;
//		maketransparent ();
//		checkobject ();
//		resettransparent ();
//		
//	}
//	public void checkobject(){
//		RaycastHit[] Hits = null;
//		Vector3 targetposition = target.position + new Vector3 (0, 3.0f, 0);
//		Vector3 direction = (targetposition - Camera.main.transform.position).normalized;
//		float distance = Vector3.Distance (Camera.main.transform.position, targetposition);
//		Ray raycast = new Ray (Camera.main.transform.position, targetposition);
//		Hits = Physics.RaycastAll (raycast, distance, targetlayer);
//		Debug.DrawLine (Camera.main.transform.position, targetposition, Color.black);
//		foreach (RaycastHit hit in Hits) {
//
//			renderers = hit.collider.GetComponentsInParent<Renderer>();  
//			foreach (Renderer r in renderers) {  
//				addtransparent (r); 
//			}  
//		}
//			
//	}
//
//	public void maketransparent(){
//		var var = transparentdictionary.GetEnumerator ();
//		while (var.MoveNext ()) {
//			temptransparent temp = var.Current.Value;
//			temp.istransparent = false;
//			foreach (Material material in temp.materials) {
//				Color color = material.GetColor ("_Color");
//				temp.fadetime += Time.deltaTime;
//				float time = temp.fadetime / fadetime;
//				color.a = Mathf.Lerp (1, Transparentnmber, time);
//				material.SetColor ("_Color", color);
//			}
//		}
//	}
//
//
//	public void resettransparent(){
//		reset.Clear ();
//		var var = transparentdictionary.GetEnumerator ();
//		while (var.MoveNext ()) {
//			if (var.Current.Value.istransparent == false) {
//				var.Current.Key.materials = var.Current.Value.sharedmaterials;
//				reset.Add (var.Current.Key);
//			}
//		}
//		foreach (var v in reset) {
//			transparentdictionary.Remove (v);
//		}
//
//	}
//
//	public void addtransparent(Renderer renderder){
//		temptransparent temp = new temptransparent();  
//			//transparentDic.Add(renderer, param);
//		transparentdictionary.Add(renderder,temp);
//		temp.sharedmaterials = renderder.sharedMaterials;  
//		temp.materials = renderder.materials;  
//		foreach(Material q in temp.materials)  
//		{  
//			q.shader = Shader.Find("ApcShader/OcclusionTransparent");  
//		}  
//		temp.istransparent = true;
//	}
//}

using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
using System.Linq;  

public class temp : MonoBehaviour {  

	public class TransparentParam  
	{  
		public Material[] materials = null;  
		public Material[] sharedMats = null;  
		public float currentFadeTime = 0;  
		public bool isTransparent = true;  
	}  

	public Transform targetObject = null;   //目标对象  
	public float height = 3.0f;             //目标对象Y方向偏移  
	public float destTransparent = 0.2f;    //遮挡半透的最终半透强度，  
	public float fadeInTime = 1.0f;         //开始遮挡半透时渐变时间  
	private int transparentLayer;           //需要遮挡半透的层级  
	public Dictionary<Renderer, TransparentParam> transparentDic = new Dictionary<Renderer, TransparentParam>();  
	public List<Renderer> clearList = new List<Renderer>();  

	void Start ()  
	{  
		transparentLayer = 1 << LayerMask.NameToLayer("OcclusionTran");  
	}  

	void Update ()  
	{  
		if (targetObject == null)  
			return;  
		UpdateTransparentObject();  
		UpdateRayCastHit();  
		RemoveUnuseTransparent();  
	}  

	public void UpdateTransparentObject()  
	{  
		var var = transparentDic.GetEnumerator();  
		while (var.MoveNext())  
		{  
			TransparentParam param = var.Current.Value;  
			param.isTransparent = false;  
			foreach (var mat in param.materials)  
			{  
				Color col = mat.GetColor("_Color");  
				param.currentFadeTime += Time.deltaTime;  
				float t = param.currentFadeTime / fadeInTime;  
				col.a = Mathf.Lerp(1, destTransparent, t);  
				mat.SetColor("_Color", col);  
			}  
		}  
	}  

	public void UpdateRayCastHit()  
	{  
		RaycastHit[] rayHits = null;  
		//视线方向为从自身（相机）指向目标位置  
		Vector3 targetPos = targetObject.position + new Vector3(0, height, 0);  
		Vector3 viewDir = (targetPos - Camera.main.transform.position).normalized;  
		Vector3 oriPos = transform.position;  
		float distance = Vector3.Distance(oriPos, targetPos);  
		Ray ray = new Ray(oriPos, viewDir);  
		rayHits = Physics.RaycastAll(ray, distance, transparentLayer);  
		//直接在Scene画一条线，方便观察射线  
		Debug.DrawLine(oriPos, targetPos, Color.yellow);  
		foreach (var hit in rayHits)  
		{  
			Renderer[] renderers = hit.collider.GetComponentsInChildren<Renderer>();  
			foreach (Renderer r in renderers)  
			{  
				AddTransparent(r);  
			}  
		}  
	}  

	public void RemoveUnuseTransparent()  
	{  
		clearList.Clear();  
		var var = transparentDic.GetEnumerator();  
		while(var.MoveNext())  
		{  
			if (var.Current.Value.isTransparent == false)  
			{  
				//用完后材质实例不会销毁，可以被unloadunuseasset销毁或切场景销毁。  
				var.Current.Key.materials = var.Current.Value.sharedMats;  
				clearList.Add(var.Current.Key);  
			}  
		}  
		foreach(var v in clearList)  
			transparentDic.Remove(v);  
	}  

	void AddTransparent(Renderer renderer)  
	{  
		TransparentParam param = null;  
		transparentDic.TryGetValue(renderer, out param);  
		if (param == null)  
		{  
			param = new TransparentParam();  
			transparentDic.Add(renderer, param);  
			//此处顺序不能反，调用material会产生材质实例。  
			param.sharedMats = renderer.sharedMaterials;  
			param.materials = renderer.materials;  
			foreach(var v in param.materials)  
			{  
				v.shader = Shader.Find("ApcShader/OcclusionTransparent");  
			}  
		}  
		param.isTransparent = true;  
	}  
}  
