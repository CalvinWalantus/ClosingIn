using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class FreelookFindPlayer : MonoBehaviour {

	public float x_constant, y_constant, x_scalar = 1000, y_scalar = 1000;

	public Slider Xsensitivity, Ysensitivity;
	// Use this for initialization
	void Start () {

		x_constant = GetComponent<CinemachineFreeLook>().m_XAxis.m_MaxSpeed;
		y_constant = GetComponent<CinemachineFreeLook>().m_YAxis.m_MaxSpeed;
		ValueChangeCheckX();
		ValueChangeCheckY();

		/*// These seemingly arbitrary operations are to solve the problem of the build being way under-sensitive in
		// the final build
		float x_original = GetComponent<CinemachineFreeLook> ().m_XAxis.m_MaxSpeed;
		float y_original = GetComponent<CinemachineFreeLook> ().m_YAxis.m_MaxSpeed;

		GetComponent<CinemachineFreeLook> ().m_XAxis.m_MaxSpeed = 1000;
		GetComponent<CinemachineFreeLook> ().m_YAxis.m_MaxSpeed = 15;

		#if UNITY_EDITOR
		GetComponent<CinemachineFreeLook> ().m_XAxis.m_MaxSpeed = x_original;
		GetComponent<CinemachineFreeLook> ().m_YAxis.m_MaxSpeed = y_original;
		#endif
		*/

		Xsensitivity.onValueChanged.AddListener(delegate {ValueChangeCheckX(); });
		Ysensitivity.onValueChanged.AddListener(delegate {ValueChangeCheckY(); });

	}

	// Invert mouse controls when called by the level manager
	public void ToggleInvertMouseY () {
		GetComponent<CinemachineFreeLook> ().m_YAxis.m_InvertAxis = !GetComponent<CinemachineFreeLook> ().m_YAxis.m_InvertAxis;
	}

	void ValueChangeCheckX() {
		float value = x_constant + x_scalar * Xsensitivity.value;
		GetComponent<CinemachineFreeLook>().m_XAxis.m_MaxSpeed = (value > 0)? value : 10;

	}

	void ValueChangeCheckY() {
		float value = y_constant + y_scalar * Ysensitivity.value;
		GetComponent<CinemachineFreeLook>().m_YAxis.m_MaxSpeed = (value > 0)? value : 10;
	}
}
