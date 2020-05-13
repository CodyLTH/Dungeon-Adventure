using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	#region Singleton
	public static GameManager instance;
	void Awake()
	{
		if (instance == null)
		{
			DontDestroyOnLoad(gameObject);
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}
	}
	#endregion


	SaveSystem saveSystem;
	public float maxHealth;
	public float currentHealth;
	public AudioSource normalBGM;
	public AudioSource clearBGM;
	public AudioSource bossBGM;
	public float startTime;

	private void Start()
	{
		CameraController camController = Camera.main.GetComponent<CameraController>();
		SettingData data = SaveSystem.instance.LoadData();
		if (data != null)
		{
			AudioListener.volume = data.volume;
			camController.mouseSensitivity = data.mouseSensitivity;
		}
		else
		{
			SaveSystem.instance.SaveData(7.5f, 1f);
		}
	}



	public void SetTime()
	{
		startTime = Time.time;
	}
	public void playNormalBGM()
	{
		clearBGM.Stop();
		bossBGM.Stop();
		normalBGM.Play();
	}
	public void playClearBGM()
	{
		normalBGM.Stop();
		bossBGM.Stop();
		clearBGM.Play();

	}
	public void playBossBGM()
	{
		normalBGM.Stop();
		clearBGM.Stop();
		bossBGM.Play();

	}
	private void OnApplicationQuit()
	{
		CameraController camController = Camera.main.GetComponent<CameraController>(); 
		SaveSystem.instance.SaveData(camController.mouseSensitivity, AudioListener.volume);
	}
}
