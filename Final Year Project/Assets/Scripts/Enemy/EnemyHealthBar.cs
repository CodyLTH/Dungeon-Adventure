using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHealthBar : MonoBehaviour
{
	// Start is called before the first frame update

	public Image healthBar;

	float max;
	float current;

	void Start()
	{
		max = GetComponentInParent<EnemyState>().health;
	}

	// Update is called once per frame
	void LateUpdate()
	{
		transform.rotation = Camera.main.transform.rotation;
		current = GetComponentInParent<EnemyState>().currentHealth;
		healthBar.fillAmount = current / max;
		healthBar.color = Color.HSVToRGB(current / (3 * max), 1, 1);
		if(healthBar.fillAmount == 0)
		{
			gameObject.SetActive(false);
		}
	}
}