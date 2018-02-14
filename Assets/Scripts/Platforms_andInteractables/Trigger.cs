using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif

public class Trigger : MonoBehaviour 
{
	public int id = 0;

	public delegate void Hit(int trigger_id);
	public event Hit hitEvent;

	public string name;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider other) 
	{
		if (other.tag.Equals("Player")) 
		{
			//hitEvent (id);
			Application.LoadLevel(name);
			#if UNITY_EDITOR
			EditorSceneManager.LoadScene(name);
			#endif
		}
	}
}
