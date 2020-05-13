using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
	SettingData data;
	public GameObject settingMenu;
	public Slider volumeSlider;
	public Slider sensitivitySlider;
	CameraController camController;

	private void Start()
	{
		camController = Camera.main.GetComponent<CameraController>();
		data = SaveSystem.instance.LoadData();
		if(data != null)
		{
			Debug.Log("Load volume" + data.volume);
			Debug.Log("Load Sens" + data.mouseSensitivity);
			AudioListener.volume = data.volume;
			camController.mouseSensitivity = data.mouseSensitivity;
			volumeSlider.value = AudioListener.volume * 10;
			sensitivitySlider.value = camController.mouseSensitivity * 2;
		}
		else
		{
			SaveSystem.instance.SaveData(camController.mouseSensitivity, AudioListener.volume);
		}
	
	}

	public void Back()
	{
		settingMenu.SetActive(false);
	}


	public void UpdateSetting()
	{
		if (!settingMenu.activeSelf)
		{
			return;
		}
		AudioListener.volume = volumeSlider.value/10;
		if (camController != null)
		{
			camController.mouseSensitivity = sensitivitySlider.value / 2;
		}
	}

	private void OnDestroy()
	{
		SaveSystem.instance.SaveData(camController.mouseSensitivity, AudioListener.volume);
	}
}
