using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif

public class PauseMenu : MonoBehaviour 
{
	public static bool paused_game = false;

	public GameObject pauseMenuUI;
	public GameObject optionsMenuUI;

	public string level = "StartScreen";
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(paused_game)
			{
				Resume();
			}
			else
			{
				Pause();
			}
		}
	}

	// For Resume button
	public void Resume()
	{
		Debug.Log ("wow");
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		paused_game = false;
	}
		
	void Pause()
	{
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		paused_game = true;
	}

	// For Restart button
	public void LoadMenu()
	{
		Debug.Log("Loading menu...");

		Time.timeScale = 1f;

		SceneManager.LoadScene(SceneManager.GetActiveScene().name);

		#if UNITY_EDITOR
		EditorSceneManager.LoadScene(EditorSceneManager.GetActiveScene().name);
		#endif
	}

	// For Quit Button
	public void QuitGame()
	{
		Debug.Log("Quitting game...");
		Application.Quit();

		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#endif
	}
	public void options(){
		pauseMenuUI.SetActive (false);
		optionsMenuUI.SetActive (true);
	}

	public void OptionsMenu(bool clicked)
	{
		if (clicked == true) 
		{
			optionsMenuUI.gameObject.SetActive (clicked);
			pauseMenuUI.gameObject.SetActive(false);
		} 
		else 
		{
			optionsMenuUI.gameObject.SetActive(clicked);
			pauseMenuUI.gameObject.SetActive(true);
		}
	}
}
