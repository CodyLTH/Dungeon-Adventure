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
		camController = Camera.main.GetComponent<CameraController>();
		volumeSlider.value = AudioListener.volume*10;
		sensitivitySlider.value = camController.mouseSensitivity*2;
	}

	public void Back()
	{
		settingMenu.SetActive(false);
	}


	public void UpdateSetting()
	{
		AudioListener.volume = volumeSlider.value/10;
		camController.mouseSensitivity = sensitivitySlider.value/2;
	}
}
