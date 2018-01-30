using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

	public bool moving = true;

	public float velocity = 5.0f;

	public Transform destination;

	Vector3 origin, current_destination;

	// Use this for initialization
	void Start () {
		origin = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
		current_destination = destination.position;
	}
	
	// Update is called once per frame

	void Update () {
		//transform.Translate(transform.forward*Mathf.Cos (Time.time)*Time.deltaTime*5);

		if (moving) {
			float step = velocity * Time.deltaTime;
			//Debug.Log ("destination: " + current_destination.position);
			//Debug.Log ("location: " + gameObject.transform.position);
			transform.position = Vector3.MoveTowards (gameObject.transform.position, current_destination, step);
			if (V3Equal(current_destination, gameObject.transform.position)) {
				if (V3Equal(current_destination, destination.position)) {
					Debug.Log ("hi"+ origin);
					current_destination = origin;
				} else {
					current_destination = destination.position;
				}
			}
		}
	}

	public bool V3Equal(Vector3 a, Vector3 b){
		return Vector3.SqrMagnitude(a - b) < 0.01;
	}
}
