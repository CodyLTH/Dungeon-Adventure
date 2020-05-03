using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItem : MonoBehaviour
{
	public float ammount = 100f;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == Player.instance.gameObject)
		{
			Player.instance.state.Heal(ammount);
			Destroy(gameObject);
		}
	}
}
