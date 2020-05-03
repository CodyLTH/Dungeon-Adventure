using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
	public bool goal;
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			GameManager.instance.currentHealth = Player.instance.state.currentHealth;
			if (goal)
			{
				FadeUI.instance.StartFade("Goal");
			}
			else
			{
				FadeUI.instance.StartFade("NextLevel");
			}
		}		
	}
}
