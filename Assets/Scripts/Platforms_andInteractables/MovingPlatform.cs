using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : ComplexCompressable {

	public bool moving = true;
	public bool debug = false;

	// Determines how long it takes the moving platform to make one trip.
	// We choose this method over setting velocity directly to help time 
	// multiple moving platforms

	public float moveTime = 4.0f;
	float velocity;

	public Transform destination;
	public Trigger trigger;

	Vector3 origin, current_destination;

	// Use this for initialization
	void Start () {
		
		origin = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
		current_destination = destination.position;

		if (trigger != null && trigger.gameObject.activeSelf) {
			trigger.hitEvent += TriggerHit;
			moving = false;
		}

		velocity = Vector3.Distance (destination.position, transform.position) / moveTime;
	}
	
	// Update is called once per frame

	void Update () {
		//transform.Translate(transform.forward*Mathf.Cos (Time.time)*Time.deltaTime*5);

		if (moving) {
			float step = velocity * Time.deltaTime;
			if (step == 3)
				moving = false;
			transform.position = Vector3.MoveTowards (gameObject.transform.position, current_destination, step);
			if (V3Equal(current_destination, gameObject.transform.position)) {
				if (V3Equal(current_destination, destination.position)) {
					current_destination = origin;
				} else {
					current_destination = destination.position;
				}
			}
		}
	}

	// Possibly should be put in a header file?
	bool V3Equal(Vector3 a, Vector3 b) {
		if (Vector3.SqrMagnitude (a - b) < 0.00000001) {
			return true;
		} else {
			return false;
		}
	}

	public void TriggerHit (int trigger_id) {
		moving = true;
	}

	public override void ComplexCompress (int two_shot, Vector3 player_position) {

		if (two_shot % 2 != 1)
			transform.position = new Vector3(transform.position.x, transform.position.y, player_position.z);
		else 
			transform.position = new Vector3(player_position.x, transform.position.y, transform.position.z);
	}

	public override void ComplexDecompress (Vector3 original) {
		// TO DO: Return switch, platform and destination to their relevant position,
		// depending on whether or not they are tagged as compressable.
	}
}
