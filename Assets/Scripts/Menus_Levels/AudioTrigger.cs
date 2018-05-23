// AudioTrigger.cs - Kris Paz and Calvin Walantus
// This object changes the volumes of the groups listed in mixerGroupList to their associated values of
// the same index in volumeList. Both of these lists can be edited per instance in the inspector.
// Note - the volume of the mixer group that you are attempting to change must be exposed, and the exposed
// parameter must have the same name as the one listed in mixerGroupList.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioTrigger:MonoBehaviour
{
	public AudioMixer master_mixer;

    public float blendTime = 1.0f;

    public List<string> mixerGroupList;
    public List<float> volumeList;

	private Dictionary<AudioMixerGroup, float>[] channels;

    public void OnTriggerEnter (Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            for (int i = 0; i < mixerGroupList.Count; i++)
            {
                float startValue;
                master_mixer.GetFloat(mixerGroupList[i], out startValue);
                StartCoroutine(VolumeLerp(mixerGroupList[i], startValue, volumeList[i], blendTime));
            }
        }
    }

    private IEnumerator VolumeLerp(string group, float src, float dst, float duration)
    {
        float startTime = Time.time;

        while (Time.time - startTime <= duration)
        {
            master_mixer.SetFloat(group, Mathf.Lerp(src, dst, (Time.time - startTime) / duration));
            yield return 1;
        }
    }


}
