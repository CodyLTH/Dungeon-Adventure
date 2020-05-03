using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameOver : MonoBehaviour
{
	public GameObject gameOverUI;
	bool flag = false;
	void Update()
	{
		if (!flag && Player.instance.state.currentHealth <= 0)
		{
			FadeUI.instance.StartFade("Die");
			flag = true;
		}
	}

	public void showUI()
	{
		gameOverUI.SetActive(true);
		GetComponent<CursorLock>().ShowCursor();
	}

	public void Menu()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene("StartMenu");
	}

	public void Respawn()
	{
		gameOverUI.SetActive(false);
		Player.instance.state.currentHealth = Player.instance.state.maxHealth;
		Player.instance.state.isDead = false;
		Player.instance.state.isInvincible = true;
		flag = false;
		Player.instance.state.RespwanMove();
		FadeUI.instance.FadeOut();
		GetComponent<CursorLock>().LockCursor();
	}




}
