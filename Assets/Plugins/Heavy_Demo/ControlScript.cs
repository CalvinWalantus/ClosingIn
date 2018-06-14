using UnityEngine;
using System.Collections;

public class ControlScript : MonoBehaviour {

	public Hv_Heavy_demo_AudioLib HeavyScript;

	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown ("space"))
			
		{
            //HeavyScript.SendBangToReceiver ("onOff");
            HeavyScript.SendEvent(Hv_Heavy_demo_AudioLib.Event.Bang);
			//print ("space pressed");
		}
		
		else if (Input.GetKeyDown ("q"))

		{
			//HeavyScript.SendFloatToReceiver (0.75f);
			//print ("space pressed");
		}

		else if (Input.GetKey ("a"))
			
		{
            float current = HeavyScript.GetFloatParameter(Hv_Heavy_demo_AudioLib.Parameter.Freq);
            HeavyScript.SetFloatParameter(Hv_Heavy_demo_AudioLib.Parameter.Freq, current -= 2);
			//print ("space pressed");
		}

		else if (Input.GetKey ("d"))
			
		{
            float current = HeavyScript.GetFloatParameter(Hv_Heavy_demo_AudioLib.Parameter.Freq);
            HeavyScript.SetFloatParameter(Hv_Heavy_demo_AudioLib.Parameter.Freq, current += 2);
            //print ("space pressed");
        }



		else
			{
			return;
			}
		}

}


