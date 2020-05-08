using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
	public GameObject settingMenu;
	public Slider volumeSlider;
	public Slider sensitivitySlider;
	CameraController camController;

	private void Start()
	{
		volumeSlider.value = AudioListener.volume * 10;

		camController = Camera.main.GetComponent<CameraController>();
		camController.mouseSensitivity = GameManager.instance.mouseSensitivity;
		sensitivitySlider.value = GameManager.instance.mouseSensitivity*2;
	}

	public void Back()
	{
		settingMenu.SetActive(false);
	}


	public void UpdateSetting()
	{
		AudioListener.volume = volumeSlider.value/10;
		if (camController != null)
		{
			camController.mouseSensitivity = sensitivitySlider.value / 2;
		}
	}

	private void OnDestroy()
	{
		GameManager.instance.mouseSensitivity = camController.mouseSensitivity;
	}

}
