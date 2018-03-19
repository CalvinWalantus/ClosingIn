using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour 
{
	
	ThirdPersonCharacter tpc;
	Animator anim;
	AudioSource audio_source;
	float animationlength;
	float timer;
	float animationspeed;
	// Use this for initialization
	void Start ()
	{
		tpc = GetComponentInParent<ThirdPersonCharacter> ();
		Debug.Log (tpc.gameObject.name);
		anim = GetComponent<Animator> ();
		audio_source = GetComponent<AudioSource> ();
		audio_source.pitch = 0.65f;
		foreach (AnimationClip i in anim.runtimeAnimatorController.animationClips) {
			//Debug.Log ("apprentspeed " + i.apparentSpeed + i.name);
			//Debug.Log ("averagespeed " + i.averageSpeed + i.);
			if (i.name == "CandleWalk") {
				animationlength = i.length;
			}
		}
		//foreach(AnimationState i in anim.runtimeAnimatorController

	}
	
	// Update is called once per frame
	void Update () 
	{
		//Debug.Log (animationlength);
		if(tpc.GetGroundStatus() == true && anim.GetCurrentAnimatorStateInfo(0).IsName("Walking") && timer > animationlength/anim.GetCurrentAnimatorStateInfo(0).speed)
		{
			audio_source.Play();
			timer = 0f;
		}
		timer += Time.deltaTime;

	}
}
