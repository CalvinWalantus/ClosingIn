using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour 
{
	bool fade;
	public AudioSource levelmusic;
	void Awake()
	{
		fade = false;
		DontDestroyOnLoad(transform.gameObject);
	}
	/*void Update(){
		if (fade) {
			StartCoroutine (fadeout (this.gameObject));
		}
	}*/


	public IEnumerator fadeout()
	{
		float original = GetComponent<AudioSource> ().volume;

		for (float i = 0.0f; i < 1.0f; i += Time.deltaTime / 22.0f)
		{
			GetComponent<AudioSource>().volume = Mathf.Lerp (original, 0, i);
			yield return 1;
		}

		if (GetComponent<AudioSource>().volume< 0.02f)
		{
			GetComponent<AudioSource>().Stop();
			levelmusic.Play();
			levelmusic.loop = true;
			yield break;
		}
	}

}
