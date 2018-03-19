using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour 
{
	public bool fade;
	public AudioSource levelmusic;
	void Awake()
	{
		fade = false;
		DontDestroyOnLoad(transform.gameObject);
	}
	void Update(){
		if (fade) {
			StartCoroutine (fadeout (this.gameObject));
		}
	}
	IEnumerator fadeout(GameObject temp){
		float original = temp.GetComponent<AudioSource> ().volume;
		for (float i = 0.0f; i < 1.0f; i += Time.deltaTime / 22.0f) {
			temp.GetComponent<AudioSource> ().volume = Mathf.Lerp (original, 0, i);
			yield return 1;
		}
		if (temp.GetComponent<AudioSource> ().volume < 0.02f) {
			temp.GetComponent<AudioSource> ().Stop ();
			levelmusic.Play ();
			levelmusic.loop = true;
			yield break;
		}
	}

}
