using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class FadeUI : MonoBehaviour
{
	#region Singletons

	public Image fadeImage;
	public GameObject canvas;
	public static FadeUI instance;
	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}
	}
	#endregion

	string mode;

	public void StartFade(string m)
	{
		mode = m;
		Animator anim = GetComponent<Animator>();
		if (m == "Goal")
		{
			fadeImage.color = Color.white;
		}
		else
		{
			fadeImage.color = Color.black;
		}
		anim.Play("FadeIn");
	}

	public void FadeOut()
	{
		Animator anim = GetComponent<Animator>();
		anim.Play("FadeOut", -1, 0f);
	}




	void FadeEvent()
	{
		switch (mode)
		{
			case "Respawn":
				Player.instance.state.RespwanMove();
				break;
			case "Die":
				canvas.GetComponent<GameOver>().showUI();
				break;
			case "NextLevel":
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
				break;
			case "Goal":
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
				break;
			case "Menu":
				SceneManager.LoadScene("StartMenu");
				break;
		}
	}
}
