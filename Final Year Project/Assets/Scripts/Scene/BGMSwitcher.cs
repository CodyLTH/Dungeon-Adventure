using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMSwitcher : MonoBehaviour
{
	public int index;
	public bool onStart = true;

	bool flag = true;



	void Start()
    {
		if (onStart)
		{
			PlayBGM();
		}
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == Player.instance.gameObject && flag)
		{
			PlayBGM();
			flag = false;
		}
	}


	void PlayBGM()
	{
		if (index == 1)
		{
			GameManager.instance.playClearBGM();
		}
		else if (index == 0)
		{
			GameManager.instance.playNormalBGM();
		}
		else if (index == 2)
		{
			GameManager.instance.playBossBGM();
		}
	}
}

