using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif

public class LevelManager : MonoBehaviour 
{
	public Transform main_menu, options_menu;

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
	public void QuitGame()
	{
		Application.Quit();

		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#endif
	}
}
