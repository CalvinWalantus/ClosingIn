using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyTrigger : MonoBehaviour {

    void OnTriggerEnter (Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            GetComponent<Hv_RandomDrone4_AudioLib>().SendEvent(Hv_RandomDrone4_AudioLib.Event.Offevent);
            Debug.Log("Onoff");
        }

    }
}
