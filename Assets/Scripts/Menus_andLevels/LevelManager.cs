using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Cinemachine;

#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif

public class LevelManager : MonoBehaviour 
{
	public Transform main_menu, options_menu, hourglass;
	public CinemachineVirtualCamera look;
	public GameObject music;
	public bool fade;
	public GameObject mainobject;
	void Start (){
		fade = false;
	}
	void Awake()
	{
		look.Priority = 40;
	}
	void Update (){
		if (fade) {
			StartCoroutine (fadeout (music));
		}
	}
	// Loads a scene when a player clicks Play button. 
	public void LoadScene(string name)
	{
		Canvas canvas = mainobject.GetComponent<Canvas> ();
		canvas.enabled = false;
		fade = true;
		look.Priority = -100;
		//this.gameObject.SetActive (false);
	}

	// Opens Option menu when player clicks Options button.
	public void OptionsMenu(bool clicked)
	{
		if (clicked == true) 
		{
			options_menu.gameObject.SetActive (clicked);
			hourglass.gameObject.SetActive(false);
			main_menu.gameObject.SetActive(false);
		} 
		else 
		{
			options_menu.gameObject.SetActive(clicked);
			hourglass.gameObject.SetActive(true);
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
	IEnumerator fadeout(GameObject temp){
		float original = temp.GetComponent<AudioSource> ().volume;
		for (float i = 0.0f; i < 1.0f; i += Time.deltaTime / 22.0f) {
			temp.GetComponent<AudioSource> ().volume = Mathf.Lerp (original, 0, i);
			yield return 1;
		}
		if (temp.GetComponent<AudioSource> ().volume < 0.02f) {
			temp.GetComponent<AudioSource> ().Stop ();
			yield break;
		}
	}
			
		

}
