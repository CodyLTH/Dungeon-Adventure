using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	#region Singleton
	public PlayerState state;
	public PlayerController controller;
	public static Player instance;
	void Awake()
	{

		if (instance == null)
		{
			instance = this;
			state = instance.GetComponent<PlayerState>();
			controller = instance.GetComponent<PlayerController>();
		}
		else
		{
			Destroy(gameObject);
		}
	}
	#endregion
}
