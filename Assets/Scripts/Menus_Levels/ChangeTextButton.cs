using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTextButton : MonoBehaviour
{
	public float timer = 0f;
	public Text txt;
	public Color clr;

	void Start()
	{
		txt = gameObject.GetComponent<Text>();
		clr = txt.color;
	}

	void Update()
	{
		//timer += Time.deltaTime;

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast (ray))
		{
			Color new_color = Color.Lerp(txt.color, Color.white, 4);
			txt.color = new_color;
		}


		/*if (timer >= 0.2f)
		{
			Color new_color = Color.Lerp(txt.color, Color.white, 4);
			txt.color = new_color;

			timer = 0;
		}*/
	}
}
