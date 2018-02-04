using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMagnet : MonoBehaviour 
{
	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("Player") || other.CompareTag ("SticksToPlatform")) {
			other.transform.parent = gameObject.transform.parent.parent;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag ("Player") || other.CompareTag ("SticksToPlatform")) {
			if (other.transform.parent.Equals (transform.parent.parent)) {
				other.transform.parent = null;
			}
		}
	}
}
