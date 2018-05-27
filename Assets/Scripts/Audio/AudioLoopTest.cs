// To test if isPlaying returns false between loops
// of a clip that is looping. 

// Experiment confirms it does not, however it does come back false for
// one frame at the very start of the play

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLoopTest : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if (GetComponent<AudioSource>().isPlaying)
        {
            Debug.Log("on");
        }
        else
        {
            Debug.Log("off");
        }
	}
}
