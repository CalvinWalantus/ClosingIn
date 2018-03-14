using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {

	public int id = 0;

	Color start_color;
	Material trigger_switch;
	bool once_only = true;

	public delegate void Hit(int trigger_id);
	public event Hit hitEvent;

	// Use this for initialization
	void Start () 
	{
		trigger_switch = gameObject.GetComponent<MeshRenderer>().material;
		start_color = trigger_switch.GetColor("_EmissionColor");
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider other) 
	{
		if (other.tag.Equals("Player") && once_only) 
		{
			hitEvent (id);
			StartCoroutine(FeedbackLerp(start_color, Color.grey, 3));
			trigger_switch.SetColor ("_EmissionColor", Color.gray);

			GetComponent<AudioSource>().PlayDelayed(0.5f);
			once_only = false;
		}
	}

	private IEnumerator FeedbackLerp(Color src, Color dst, float duration)
	{
		float startTime = Time.time;

		while (Time.time - startTime < duration) 
		{
			trigger_switch.SetColor("_EmissionColor", Color.Lerp(src, dst, (Time.time - startTime) / duration));
			yield return 1;
		}
	}
}
