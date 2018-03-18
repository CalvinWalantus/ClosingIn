using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour 
{
	ThirdPersonCharacter tpc;
	Animator anim;
	AudioSource audio_source;

	// Use this for initialization
	void Start () 
	{
		tpc = GetComponentInParent<ThirdPersonCharacter>();
		anim = GetComponent<Animator>();
		audio_source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(tpc.GetGroundStatus() == true && anim.GetCurrentAnimatorStateInfo(0).IsName("Walking") && audio_source.isPlaying == false)
		{
			audio_source.PlayDelayed(1f);
		}
	}
}
