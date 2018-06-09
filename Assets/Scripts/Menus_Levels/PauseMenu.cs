using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif

public class PauseMenu : MonoBehaviour 
{
	public LevelManager level_manager;

	public static bool paused_game = false;

	public GameObject pauseMenuUI;
	public GameObject optionsMenuUI;

	public string level = "StartScreen";
	
	void Start()
	{
		level_manager = (LevelManager)FindObjectOfType(typeof(LevelManager));
	}

	// Update is called once per frame
	void Update() 
	{
		if(level_manager.key_enabled)
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				if (paused_game)
				{
					Resume ();
				} 
				else
				{
					Pause ();
				}
			}
		} 
		else 
		{
		}
	}

	// For Resume button
	public void Resume()
	{
		level_manager.key_enabled = true;

		pauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		paused_game = false;
	}
		
	void Pause()
	{
		level_manager.key_enabled = false;

		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		paused_game = true;
	}

	// For Restart button
	public void LoadMenu()
	{
		Time.timeScale = 1f;

		SceneManager.LoadScene(SceneManager.GetActiveScene().name);

		#if UNITY_EDITOR
		EditorSceneManager.LoadScene(EditorSceneManager.GetActiveScene().name);
		#endif
	}

	// For Quit Button
	public void QuitGame()
	{
		Application.Quit();

		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#endif
	}
		
	public void OptionsMenu(bool clicked)
	{
		if (level_manager.menu_state == false) // is Gameplay Pause = False
		{
			if (clicked == true)
			{
				optionsMenuUI.gameObject.SetActive (clicked);
				pauseMenuUI.gameObject.SetActive (false);
			} 
		}
	}
}
