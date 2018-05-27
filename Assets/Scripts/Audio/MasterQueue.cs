// MasterQueue.cs - Calvin Walantus
// Handles non-diegetic music. takes input from the various AudioTriggers placed throughout
// the level. The public array startingAudio determines what audio plays at the start of the game.
// This script should be attached to the WorldController for convenience - or anywhere, really.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterQueue : MonoBehaviour {

    AudioSource currentlyPlaying;

    public AudioSource[] startingAudio;

	// Queue starting audio
	void Start () {
		if (startingAudio != null)
        {
            foreach (AudioSource audio in startingAudio)
            {
                QueueAudio(audio);
            }
        }
	}

    // Play audio if nothing is playing, or queue it if something is still playing
    // This function is called by audiotriggers
    public void QueueAudio (AudioSource audio)
    {
        if (currentlyPlaying == null)
        {
            audio.gameObject.SetActive(true);
            currentlyPlaying = audio;
        }
        else
        {
            StartCoroutine(PlayAfterCurrent(audio));
        }
    }

    // Waits for previous clip to end, cancelling loop.
    private IEnumerator PlayAfterCurrent (AudioSource audio)
    {
        currentlyPlaying.loop = false;
        while (currentlyPlaying.isPlaying)
        {
            yield return 1;
        }

        currentlyPlaying.gameObject.SetActive(false);
        audio.gameObject.SetActive(true);
        currentlyPlaying = audio;
    }

}
