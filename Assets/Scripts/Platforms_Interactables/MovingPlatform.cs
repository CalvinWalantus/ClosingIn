 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Hv_RandomDrone4_AudioLib))]
public class MovingPlatform : ComplexCompressable {

	public bool moving = true;
	public bool debug = false;
	private bool wait = false;
	public float waitsecond = 3;

	// Determines how long it takes the moving platform to make one trip.
	// We choose this method over setting velocity directly to help time 
	// multiple moving platforms

	public float moveTime = 4.0f;
	float velocity;

	public Transform destination;
	public Trigger trigger;

	Vector3 origin, current_destination;

    // Determines whether the platform uses the heavy audio drone
    public bool soundingPlatform = false;

    // Status of the heavy audio patch, flag used by HeavySFX function
    bool soundStatus = false;
    public float volume = 0.6f;
    float soundDistance;
    float startingDistance = 30;
    Hv_RandomDrone4_AudioLib heavyPatch;
    Transform player;


    // Use this for initialization
    void Awake ()
    {
        if (!GetComponent<Hv_RandomDrone4_AudioLib>())
        {
            heavyPatch = gameObject.AddComponent<Hv_RandomDrone4_AudioLib>();
            GetComponent<AudioSource>().maxDistance = startingDistance;
        }

        GetComponent<Hv_RandomDrone4_AudioLib>().SendEvent(Hv_RandomDrone4_AudioLib.Event.Offevent);
        soundDistance = GetComponent<AudioSource>().maxDistance;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

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

        if (soundStatus)
        {
            ChangeHeavyVolume();
        }

        if (moving) {
			float step = velocity * Time.deltaTime;
            if (step == 3) {
                moving = false;
                Debug.Log("moving = false");
            }
            if (wait == false) {
				
				transform.position = Vector3.MoveTowards (gameObject.transform.position, current_destination, step);
			}
			if (V3Equal(current_destination, gameObject.transform.position)) {
                HeavySFX(false);
                wait = true;
				if (V3Equal(current_destination, destination.position)) {
					current_destination = origin;
				} else {
					current_destination = destination.position;
				}
				StartCoroutine (WaitASecond (waitsecond));
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
        HeavySFX(true);
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

	IEnumerator WaitASecond(float second){
		yield return new WaitForSeconds (second);
		wait = false;
        HeavySFX(true);
    }

    // Turn on/off the heavy drone sound. Flag guards against repeated GetComponent calls
    void HeavySFX(bool status)
    {
        if (soundingPlatform && (soundStatus != status))
        {
            Debug.Log("soundsStatus: " + soundStatus);
            if (status)
            {
                heavyPatch.SendEvent(Hv_RandomDrone4_AudioLib.Event.Onevent);
            }
            else
            {
                heavyPatch.SendEvent(Hv_RandomDrone4_AudioLib.Event.Offevent);
            }
            soundStatus = status;
        }
    }

    // Controls linear rolloff of volume, since heavy cannot connect to
    // the audiosource
    void ChangeHeavyVolume ()
    {
        float vol = Vector3.Distance(transform.position, player.position);

        if (vol <= soundDistance)
        {
            heavyPatch.SetFloatParameter(Hv_RandomDrone4_AudioLib.Parameter.Vol, volume * (soundDistance - vol)/soundDistance);
        }
        

    }
}

