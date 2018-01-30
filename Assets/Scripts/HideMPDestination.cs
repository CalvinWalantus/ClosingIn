using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMPDestination : MonoBehaviour {

	public bool hide_position = true;

	// Use this for initialization
	void Start () {
		if (hide_position)
			GetComponent<MeshRenderer> ().enabled = false;
	}

}
