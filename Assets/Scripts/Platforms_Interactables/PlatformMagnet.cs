using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMagnet : MonoBehaviour 
{

	public bool useGrandparent = true;

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("Player") || other.CompareTag ("SticksToPlatform")) {
			if (useGrandparent) {
				other.transform.parent = gameObject.transform.parent.parent;
			} else {
				other.transform.parent = gameObject.transform.parent;
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag ("Player") || other.CompareTag ("SticksToPlatform")) {
			if (other.transform.parent.Equals (transform.parent.parent)) {
				other.transform.parent = null;
			}

            if (useGrandparent)
            {
                gameObject.transform.parent.parent.GetComponent<Hv_RandomDrone4_AudioLib>().SendEvent(Hv_RandomDrone4_AudioLib.Event.Offevent);
            }
            else
            {
                gameObject.transform.parent.GetComponent<Hv_RandomDrone4_AudioLib>().SendEvent(Hv_RandomDrone4_AudioLib.Event.Offevent);
            }
        }
	}
}
