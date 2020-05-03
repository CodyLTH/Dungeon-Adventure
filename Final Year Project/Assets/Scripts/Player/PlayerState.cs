using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerState : MonoBehaviour
{
	Animator playerAnim;
	public Transform respawnPoint;
	public float healingRate = 10;
	public float maxHealth;
	public float currentHealth;
	public float hitInvincibleTime = 0.8f;
	public float knockInvincibleTime = 2.5f;
	public bool isInvincible = false;
	public bool isDead = false;

	public float dropDamage = 100f;

	public AudioSource getHitSE;

	void Start()
    {

		maxHealth = GameManager.instance.maxHealth;
		currentHealth = GameManager.instance.currentHealth;
		playerAnim = gameObject.GetComponentInChildren<Animator>();
		playerAnim.SetBool("IsGrounded", true);
	}

	private void Update()
	{
		if (isDead || isInvincible)
		{
			return;
		}
		currentHealth += healingRate * Time.deltaTime;
		currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
	}

	public void TakeDamage(float amount, GameObject obj, bool knock)
	{
		if (isDead || isInvincible)
		{
			return;
		}
		isInvincible = true;
		currentHealth -= amount;

		if (obj != null)
		{
			var direction = obj.transform.position - transform.position;
			direction.y = 0;
			transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
		}

		if (currentHealth <= 0)
		{
				currentHealth = 0;
				isDead = true;
				playerAnim.SetBool("IsDead", true);
		}
		if (knock)
		{
			Invoke("ResetInvincibleTimeFlag", knockInvincibleTime);
			Player.instance.controller.KnockedDownAnimation();
		}
		else
		{
			if (isDead)
			{
				Player.instance.controller.DieAnimation();
			}
			else
			{
				Player.instance.controller.GetHitAnimation();
				Invoke("ResetInvincibleTimeFlag", hitInvincibleTime);
			}
		}
		getHitSE.Play();
	}

	public void Heal(float ammount)
	{
		currentHealth += ammount;
		currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
	}

	void ResetInvincibleTimeFlag()
	{
		if (!isDead)
		{
			isInvincible = false;
		}
	}

	void Die()
	{
		isDead = true;
		playerAnim.SetBool("IsDead", true);
		Debug.Log("Game Over");

	}

	void Respwan()
	{
		isInvincible = true;
		FadeUI.instance.StartFade("Respawn");
	}

	public void RespwanMove()
	{
		Invoke("ResetInvincibleTimeFlag", 1f);

		gameObject.SetActive(false);
		transform.position = respawnPoint.transform.position;
		transform.rotation = respawnPoint.transform.rotation;
		CameraController camController = Camera.main.gameObject.GetComponent<CameraController>();
		camController.pitch = 0;
		camController.yaw = transform.eulerAngles.y;
		Camera.main.transform.eulerAngles = transform.eulerAngles;
		gameObject.SetActive(true);
		playerAnim.SetBool("IsGrounded", true);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Outside"))
		{
			if (!isInvincible)
			{
				currentHealth -= dropDamage;
			}
			if (currentHealth <= 0)
			{
				currentHealth = 0;
				Die();
			}
			else
			{
				Respwan();
			}
		}
	}
}
