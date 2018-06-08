// AudioTrigger.cs - Kris Paz and Calvin Walantus
// Sends message to MasterQueue to begin playback. Audio will be played in the order it
// is sent in.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioTrigger:MonoBehaviour
{
    public AudioSource[] audioToStart;

    MasterQueue masterQ;

    public void Start()
    {
        masterQ = FindObjectOfType<MasterQueue>();
    }

    // Tell the MasterQueue to start the audio
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            foreach (AudioSource audio in audioToStart)
            {
                masterQ.QueueAudio(audio);
            }
        	Destroy(this.gameObject);
        }

    }
}
