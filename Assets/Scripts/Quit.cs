using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{
	private void Update()
	{
		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
			Debug.Break();
		}
	}
}
