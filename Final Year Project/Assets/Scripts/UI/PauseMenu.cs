using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class PauseMenu : MonoBehaviour
{

	public static bool GameIsPauesed = false;

	public GameObject pauseMenuUI;
	public GameObject fadeImage;
	public GameObject gameOverUI;
	public GameObject SettingMenu;
	public TextMeshProUGUI textMesh;


	void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (GameIsPauesed)
			{
				Resume();
			}
			else
			{
				Pause();
			}
		}
    }


	public void Resume()
	{
		Time.timeScale = 1;
		pauseMenuUI.SetActive(false);
		GetComponent<CursorLock>().LockCursor();
		SettingMenu.SetActive(false);
		textMesh.text = "Esc - Pause";
		GameIsPauesed = false;
	}

	public void Pause()
	{
		if (fadeImage.activeSelf ||gameOverUI.activeSelf )
		{
			return;
		}
		Time.timeScale = 0;
		pauseMenuUI.SetActive(true);
		GetComponent<CursorLock>().ShowCursor();
		textMesh.text = "Esc - Resume";
		GameIsPauesed = true;
	}


	public void Setting()
	{
		Time.timeScale = 0;
		SettingMenu.SetActive(true);
		GetComponent<CursorLock>().ShowCursor();
		GameIsPauesed = true;
	}



	public void Menu()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene("StartMenu");
	}

	public void Quit()
	{
		Time.timeScale = 1;
		Debug.Log("Quit Game");
		Application.Quit();
	}




}
