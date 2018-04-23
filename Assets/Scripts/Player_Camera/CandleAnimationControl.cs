using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleAnimationControl : MonoBehaviour {
	//checking if player is moving, and control candle's animations based on that.
	public Rigidbody player;
	private Vector3 curPos;
	private Vector3 lastPos;
	public Animator animate;

	public float min = 0.1f;

	// Use this for initialization
	void Start () {
		animate = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		float magnitude = new Vector2 (player.velocity.x, player.velocity.z).magnitude;
		animate.SetBool ("walking", magnitude >= 0.1);
	}
}
