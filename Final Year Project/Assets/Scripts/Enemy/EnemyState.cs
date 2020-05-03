using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
	public float health = 500f;
	public float stun = 1;
	public float currentHealth;

	float stunCount = 0;
	EnemyController enemyContoller;

	void Start()
    {
		currentHealth = health;
		stunCount = 0;
		enemyContoller = GetComponent<EnemyController>();
	}

	public void TakeDamage(float amount)
	{
		if(enemyContoller.playerFound == false)
		{
			enemyContoller.playerFound = true;
			enemyContoller.TakeDamageAnimation();
		}
		currentHealth -= amount;
		Debug.Log("Current HP = " + currentHealth);
		if (currentHealth <= 0)
		{
			currentHealth = 0;
			enemyContoller.DieAnimation();
			Invoke("Destroy", 2f);
		}
		else
		{
			stunCount++;
			if ( stunCount >= stun)
			{
				enemyContoller.TakeDamageAnimation();
				stunCount = 0;
			}
		}
	}
	
	void Destroy()
	{
		Destroy(gameObject);
	}
	
}
