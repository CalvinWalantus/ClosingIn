using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FreelookFindPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<CinemachineFreeLook> ().m_Follow = FindObjectOfType<ThirdPersonCharacter> ().transform;
		GetComponent<CinemachineFreeLook> ().m_LookAt = FindObjectOfType<ThirdPersonCharacter> ().transform;
	}
}
