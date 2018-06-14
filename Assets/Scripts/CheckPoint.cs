using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {
	public bool activated = false;
	public static CheckPoint[] checkPointsList;

	public GameObject respawnLocation;

	public ParticleSystem flame;

    public AudioSource startSound, loop;

	// Use this for initialization
	void Start () {
		checkPointsList = FindObjectsOfType<CheckPoint>();
	}

	// Update is called once per frame
	void Update () {
		if (this.activated == true && !flame.isPlaying) {
			flame.Play();
            startSound.Play();
            StartCoroutine(PlayLoopAfterStart());
		} else if (this.activated == false) {
			flame.Stop ();
            loop.Stop();
		}
		//Debug.Log (this.GetComponentInChildren<ParticleSystem> ().isPlaying);
	}

	void activateCheckPoint(){
		foreach (CheckPoint beacon in checkPointsList) {
			beacon.activated = false;
		}
		activated = true;
	}

	void OnTriggerEnter(Collider objects){
		if(objects.transform.gameObject.tag == "Player"){
			activateCheckPoint();
		}
	}

	public static Vector3 returnCheckPointLocation(){
		Vector3 result = Vector3.zero;
		foreach (CheckPoint beacon in checkPointsList) {
			if (beacon.activated == true) {
				result = beacon.respawnLocation.transform.position;
			}
		}
		return result;
	}

    private IEnumerator PlayLoopAfterStart()
    {
        while (startSound.isPlaying)
        {
            yield return 1; 
        }
        loop.Play();
    }
}
