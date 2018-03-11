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
		start_color = trigger_switch.GetColor("_Color");

		Debug.Log(start_color.ToString());
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider other) 
	{
		if (other.tag.Equals("Player") && once_only) 
		{
			hitEvent (id);
			StartCoroutine(FeedbackLerp(start_color, Color.gray, 3));
		}
	}

	private IEnumerator FeedbackLerp(Color src, Color dst, float duration)
	{
		float startTime = Time.time;

		while (Time.time - startTime < duration) 
		{
			trigger_switch.SetColor("_Color", Color.Lerp (src, dst, (Time.time - startTime) / duration));
		}
		yield return 1;
	}
}
