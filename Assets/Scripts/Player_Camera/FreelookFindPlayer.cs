using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FreelookFindPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {

		// These seemingly arbitrary operations are to solve the problem of the build being way under-sensitive in
		// the final build
		float x_original = GetComponent<CinemachineFreeLook> ().m_XAxis.m_MaxSpeed;
		float y_original = GetComponent<CinemachineFreeLook> ().m_YAxis.m_MaxSpeed;

		GetComponent<CinemachineFreeLook> ().m_XAxis.m_MaxSpeed = 1000;
		GetComponent<CinemachineFreeLook> ().m_YAxis.m_MaxSpeed = 15;

		#if UNITY_EDITOR
		GetComponent<CinemachineFreeLook> ().m_XAxis.m_MaxSpeed = x_original;
		GetComponent<CinemachineFreeLook> ().m_YAxis.m_MaxSpeed = y_original;
		#endif

	}

	// Invert mouse controls when called by the level manager
	public void ToggleInvertMouseY () {
		GetComponent<CinemachineFreeLook> ().m_YAxis.m_InvertAxis = !GetComponent<CinemachineFreeLook> ().m_YAxis.m_InvertAxis;
	}
}
