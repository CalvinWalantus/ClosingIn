using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

	public bool moving = true;

	public float velocity = 4.0f;

	public Transform destination;

	public Trigger trigger;

	Vector3 origin, current_destination;

	// Use this for initialization
	void Start () {
		
		origin = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
		current_destination = destination.position;

		if (trigger != null && trigger.gameObject.activeSelf) {
			Debug.Log ("trigger detected");
			trigger.hitEvent += TriggerHit;
			moving = false;
		}
	}
	
	// Update is called once per frame

	void Update () {
		//transform.Translate(transform.forward*Mathf.Cos (Time.time)*Time.deltaTime*5);

		if (moving) {
			float step = velocity * Time.deltaTime;
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
	bool V3Equal(Vector3 a, Vector3 b){
		return Vector3.SqrMagnitude(a - b) < 0.01;
	}

	public void TriggerHit (int trigger_id) {
		moving = true;
	}
}
