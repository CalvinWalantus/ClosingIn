﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine.Animations;
using UnityEngine;

// Considering changing the name of this to "DimensionController"
public class WorldwithAnimationEvents : MonoBehaviour
{
	// True = 3D
	// False = 2D
	public bool dimension = false;

	public int two_shot = 1;
	public int three_shot = 1;
	public int current_shot;		// Keeps track of current shot.

	public float shift_time = 5;
	public float timer = 0;

	// Event signaling a dimension change (a "shift")
	public delegate void Shift(bool dim, float time);
	public event Shift shiftEvent;

	// Event signalling a shot change
	// 1-4 = 2D shot change
	// 5-8 = 3D shot change
	public delegate void ShotChange (int tw_shot, int th_shot);
	public event ShotChange shotChangeEvent;

	// Event signaling an animation play AT START
	public delegate void AnimationStart();
	public event AnimationStart animationStartEvent;

	// Event signaling an animation play AT END
	public delegate void AnimationEnd();
	public event AnimationEnd animationEndEvent;

	public bool playAnimationOnStart;
	public float startAnimationSpeed = 2;

	void Start()
	{
		timer = shift_time;
		shotChangeEvent(two_shot, three_shot);
		shiftEvent(dimension, shift_time);

		foreach (Teleport boundary in FindObjectsOfType<Teleport>())
		{
			boundary.RespawnEvent += HandleRespawnEvent;
		}
			
		if (playAnimationOnStart)
		{
			StartCoroutine(PlayStartAnimation());
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Check if user has pressed shift to bring about a dimension shift
		if (Input.GetKeyDown(KeyCode.LeftShift) && timer > shift_time)
		{
			dimension = !dimension;
			shiftEvent(dimension, shift_time);
			timer = 0;
		}

		// If in 2D, then check between arrow input keys (up, left, right) for shot changes. 
		// Up - Across shots
		// Left / Right - Move shots left and right
		if (dimension == false && (two_shot > 0 && two_shot < 5))
		{
			ShotChangeOnInput(ref two_shot);
		} 

		// TODO: change 3d shotchange rules
		else
		{
			ShotChangeOnInput(ref three_shot);
		}

		// Check if user has pressed the 1 - 8 keys to bring about a shot change.
		for (int shot = 1; shot < 9; shot++) 
		{
			if (Input.GetKeyDown((KeyCode)shot + 48)) 
			{
				if (shot < 5) {
					two_shot = shot;
				} else {
					three_shot = shot - 4;
				}
				shotChangeEvent(two_shot, three_shot);
				break;
			}
		}

		timer += Time.deltaTime;
	}

	void HandleRespawnEvent(bool dim, int tw_shot, int thr_shot)
	{
		dimension = dim;
		two_shot = tw_shot;
		three_shot = thr_shot;

		shotChangeEvent(two_shot, three_shot);
		shiftEvent(dimension, shift_time);
	}

	void ShotChangeOnInput(ref int current_shot)
	{
		// Tracks Current Shot
		int compare = current_shot;

		if (Input.GetKeyDown(KeyCode.LeftArrow)) 
		{
			current_shot -= 1;				// Move shot left from Shot 2 to Shot 2 = Shot 2 - 1.

			if (current_shot < 1)
			{
				current_shot = 4;
			}
		} 
		else if(Input.GetKeyDown(KeyCode.RightArrow))
		{
			current_shot += 1;				// Move shot right from Shot 2 to Shot 2 = Shot 2 + 1.

			if (current_shot > 4)
			{
				current_shot = 1;
			}
		} 
		else if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			// If it's Shot 1 or 2, then add 2.
			if (current_shot < 3)
			{
				current_shot += 2;
			} 
			else 
			{
				// They were Shots 3 or 4, then subtract 2.
				current_shot -= 2;
			}
		}

		if (current_shot != compare) 
		{
			shotChangeEvent(two_shot, three_shot);
		}
	}

	private IEnumerator PlayStartAnimation()
	{
		// Trigger an "animationStart" event
		Camera.main.gameObject.GetComponent<PlayableDirector>().Play();
		animationStartEvent();

		// Trying to change aniumation speed, not working
		AnimationMixerPlayable mixer = AnimationMixerPlayable.Create (Camera.main.gameObject.GetComponent<PlayableDirector> ().playableGraph, 1);
		mixer.SetSpeed(startAnimationSpeed);

		// When animation is over, trigger an "animationEnd" event
		while (true) 
		{
			if (Camera.main.gameObject.GetComponent<PlayableDirector>().state != PlayState.Playing)
			{
				animationEndEvent();
				Debug.Log("Animation is Done.");
				yield break;
			}
			yield return 1;
		}
	}
}

