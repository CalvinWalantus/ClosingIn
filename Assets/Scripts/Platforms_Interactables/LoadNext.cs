﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif

public class LoadNext : MonoBehaviour 
{
	public string level;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void OnTriggerEnter (Collider other) 
	{
		if (other.tag.Equals("Player"))
		{
			SceneManager.LoadScene(level);
			#if UNITY_EDITOR
			EditorSceneManager.LoadScene(level);
			#endif
		}
	}
}
