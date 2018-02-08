using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour 
{
	public Transform main_menu, options_menu;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	// Loads a scene when a player clicks Play button. 
	public void LoadScene(string name)
	{
		Application.LoadLevel(name);
	}

	// Opens Option menu when player clicks Options button.
	public void OptionsMenu(bool clicked)
	{
		if (clicked == true) 
		{
			options_menu.gameObject.SetActive (clicked);
			main_menu.gameObject.SetActive (false);
		} 
		else 
		{
			options_menu.gameObject.SetActive(clicked);
			main_menu.gameObject.SetActive(true);
		}
	}

	// Quits the game when player clicks Quit button.
	// Note: Won't leave from game.
	public void QuitGame()
	{
		Application.Quit();
		UnityEditor.EditorApplication.isPlaying = false;
	}
}
