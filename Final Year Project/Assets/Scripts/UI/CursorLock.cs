using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLock : MonoBehaviour
{
	public GameObject pauseMenu;
	public GameObject dialogueUI;
	public GameObject gameOverUI;

	public void ShowCursor()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	public void LockCursor()
	{
		if (!dialogueUI.activeSelf && !pauseMenu.activeSelf && !gameOverUI.activeSelf)
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}
}
