using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
	public TextMeshProUGUI textMesh;

	float time;
	double time2;

	// Update is called once per frame
	void LateUpdate()
	{
		time = Time.time - GameManager.instance.startTime;
		if(time < 10000)
		{
			textMesh.text = "Time: " + time.ToString("F1"); ;
		}
		else
		{
			textMesh.text = "Time: N/A";
		}
	}
}
