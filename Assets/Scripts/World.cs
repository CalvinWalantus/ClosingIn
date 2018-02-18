using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Considering changing the name of this to "DimensionController"
public class World : MonoBehaviour {
	
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

	void Start() {
		timer = shift_time;
		shotChangeEvent (two_shot, three_shot);
		shiftEvent (dimension, shift_time);

		foreach (Teleport boundary in FindObjectsOfType<Teleport>())
		{
			boundary.RespawnEvent += HandleRespawnEvent;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		// Check if user has pressed shift to bring about a dimension shift
		if (Input.GetKeyDown(KeyCode.DownArrow) && timer > shift_time)
		{
			dimension = !dimension;
			shiftEvent(dimension, shift_time);
			timer = 0;
		}
		timer += Time.deltaTime;




		// If in 2D, then check between arrow input keys (up, left, right) for shot changes. 
		// Up - Across shots
		// Left / Right - Move shots left and right
		if (dimension == false && (two_shot > 0 && two_shot < 5))
		{
			ShotChangeOnInput(ref two_shot);

		} 
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
				shotChangeEvent (two_shot, three_shot);
				break;
			}
		}
	}

	void HandleRespawnEvent(bool dim, int tw_shot, int thr_shot) {
		dimension = dim;
		two_shot = tw_shot;
		three_shot = thr_shot;
		float temp = shift_time;
		shift_time = 0;
		shiftEvent (dimension, shift_time);
		shotChangeEvent (two_shot, three_shot);
		shift_time = temp;
	}



	void ShotChangeOnInput(ref int current_shot)
	{
		// Tracks Current Shot

		int compare = current_shot;

		if (Input.GetKeyDown (KeyCode.LeftArrow)) 
		{
			current_shot -= 1;				// Move shot left from Shot 2 to Shot 2 = Shot 2 - 1.

			if (current_shot < 1)
			{
				current_shot = 4;
			}
		} 
		else if(Input.GetKeyDown (KeyCode.RightArrow))
		{
			current_shot += 1;				// Move shot right from Shot 2 to Shot 2 = Shot 2 + 1.

			if (current_shot > 4)
			{
				current_shot = 1;
			}
		} 
		else if (Input.GetKeyDown (KeyCode.UpArrow))
		{
			// If it's Shot 1 or 2, then add 2.
			if (current_shot < 3)
			{
				current_shot += 2;
			} 
			else 
			{ 	// They were Shots 3 or 4, then subtract 2.
					current_shot -= 2;
			}
		}


		if (current_shot != compare) {
			Debug.Log (current_shot + " " + two_shot + " " + three_shot);
			shotChangeEvent (two_shot, three_shot);
		}
	}
}

