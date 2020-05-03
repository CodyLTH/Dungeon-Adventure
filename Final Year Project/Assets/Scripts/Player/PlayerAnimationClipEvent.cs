using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationClipEvent : MonoBehaviour
{
	Weapon weapon;

	public AudioSource footstepSE;
	public AudioSource jumpSE;
	public AudioSource landSE;

	void Start()
	{
		weapon = GetComponentInChildren<Weapon>();
	}
	public void ResetPlayerSpeed()
	{
		Player.instance.controller.ResetCurrentSpeed();
	}

	public void PlayFootstepSE()
	{
		footstepSE.Play();
	}
	public void PlayJumpSE()
	{
		jumpSE.Play();
	}
	public void PlayLandSE()
	{
		landSE.Play();
	}





}
