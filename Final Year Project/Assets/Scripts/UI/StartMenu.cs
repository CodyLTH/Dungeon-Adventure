﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
	// Start is called before the first frame update

	public void PlayGame()
	{
		FadeUI.instance.StartFade("NextLevel");
		GameManager.instance.SetTime();
		GameManager.instance.currentHealth = GameManager.instance.maxHealth;
	}

	public void Quit()
	{
		Debug.Log("Quit");
		Application.Quit();
	}


}
