using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {

	public int id = 0;

	Material start_material;
	public Material end_material;

	public float lerpSpeed = 100.0f;

	MeshRenderer rend;

	bool once_only = true;

	public delegate void Hit(int trigger_id);
	public event Hit hitEvent;
	public float delay = 0.5f;

	// Use this for initialization
	void Start () 
	{
		rend = GetComponent<MeshRenderer> ();
		start_material = rend.material;
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider other) 
	{
		if (other.tag.Equals("Player") && once_only) 
		{
			hitEvent (id);
			StartCoroutine(FeedbackLerp(start_material, end_material, lerpSpeed));

			GetComponent<AudioSource>().PlayDelayed(delay);
			once_only = false;
		}
	}

	private IEnumerator FeedbackLerp(Material src, Material dst, float duration)
	{
		float startTime = Time.time;

		while (Time.time - startTime <= duration) 
		{
			rend.material.Lerp (src, dst, (Time.time - startTime) / duration);
			yield return 1;
		}
	}
}
