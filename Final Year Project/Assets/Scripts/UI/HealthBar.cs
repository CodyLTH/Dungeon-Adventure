using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
	// Start is called before the first frame update

	public Image healthBar;
	public TextMeshProUGUI textMesh;

	float max;
	float current;
	
	void Start()
	{
		max = GameManager.instance.maxHealth;
	}

	// Update is called once per frame
	void LateUpdate()
    {
		current = Player.instance.state.currentHealth;
		healthBar.fillAmount = current / max;
		textMesh.text = "Health: " + (int)current + "/" + (int)max;
		healthBar.color = Color.HSVToRGB(current/(3*max), 1, 1);

	}
}
