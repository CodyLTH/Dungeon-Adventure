using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClearMenu : MonoBehaviour
{
	public TextMeshProUGUI textmesh;


	private void Start()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		float clearTime = Time.time - GameManager.instance.startTime;
		if (clearTime < 10000)
		{
			textmesh.text = "Clear Time: " + clearTime.ToString("F1") + " second";
		}
		else
		{
			textmesh.text = "Clear Time: N/A";
		}

	}

	public void BackToMenu()
	{
		FadeUI.instance.StartFade("Menu");
	}
}
