using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parent_animate : MonoBehaviour {
	private Rigidbody rigid;
	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (rigid.transform.position.x);
	}
}
