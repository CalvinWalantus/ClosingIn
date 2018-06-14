using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script name (c)2018 Sid Verma
public class PatchController : MonoBehaviour {

    World world_controller;
    Hv_shiftingDrone_AudioLib heavy_patch;
    float volume = 0.7f;
    
    // Use this for initialization
	void Start () {
        world_controller = GetComponent<World>();
        world_controller.shiftEvent += HandleShift;

        heavy_patch = GetComponent<Hv_shiftingDrone_AudioLib>();
        //heavy_patch.SetFloatParameter(Hv_shiftingDrone_AudioLib.Parameter.Vol, 0.0f);
	}
	
    private void HandleShift(bool dim, float time)
    {
        //heavy_patch.SetFloatParameter(Hv_shiftingDrone_AudioLib.Parameter.Vol, volume);
        heavy_patch.SendEvent(Hv_shiftingDrone_AudioLib.Event.Startfade);

        StartCoroutine(WaitForEnd(time));
    }

    private IEnumerator WaitForEnd (float time)
    {
        if (time < 0.5f)
        {
            time = 0.5f;
        }
        yield return new WaitForSeconds(time);
        heavy_patch.SendEvent(Hv_shiftingDrone_AudioLib.Event.Endfade);
    }
}
