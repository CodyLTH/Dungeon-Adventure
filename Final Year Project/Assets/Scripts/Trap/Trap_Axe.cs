using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Axe : MonoBehaviour
{
	public float damage;
	public GameObject hitCenter;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == Player.instance.gameObject)
		{
			Player.instance.state.TakeDamage(damage, hitCenter, true);
		}
	}
}
