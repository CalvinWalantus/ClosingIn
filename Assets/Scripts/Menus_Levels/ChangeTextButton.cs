using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTextButton : MonoBehaviour
{
	public Text text;

	void Start()
	{
		text = GetComponent<Text>();
		Debug.Log(text.transform);
	}

	void OnHover()
	{
		Debug.Log("This is Supposed to Change");
	}
}
