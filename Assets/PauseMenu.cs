using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour 
{
	public static bool paused_game = false;
	public GameObject pauseMenuUI;

	// Use this for initialization
	void Start () 
	{
		
	}
	
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
		EditorSceneManager.LoadScene("StartMenu_P");
	}

	// For Quit Button
	public void QuitGame()
	{
		Debug.Log("Quitting game...");
		Application.Quit();
		UnityEditor.EditorApplication.isPlaying = false;
	}
}
