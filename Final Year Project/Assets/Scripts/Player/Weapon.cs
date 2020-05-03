using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public float damage = 100f;
	public AudioSource hitSE;

	void OnTriggerEnter(Collider other)
	{
		if (Player.instance.controller.isAttacking && other.gameObject.CompareTag("EnemyCollider"))
		{
			EnemyState enemyState = other.gameObject.GetComponentInParent<EnemyState>();
			if(enemyState != null)
			{
				enemyState.TakeDamage(damage);
				hitSE.Play();
			}
		}
	}
}
