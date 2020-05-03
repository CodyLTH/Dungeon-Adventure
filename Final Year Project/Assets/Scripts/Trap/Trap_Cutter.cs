using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Cutter : MonoBehaviour
{
	public float damage;
	Collider[] colChildren;

	// Start is called before the first frame update
	void Start()
	{
		colChildren = GetComponentsInChildren<Collider>();
	}

	// Update is called once per frame
	void Update()
	{
		foreach (Collider collider in colChildren)
		{
			if (Player.instance.state.isInvincible)
			{
				collider.enabled = false;
			}
			else
			{
				collider.enabled = true;
			}
		}
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == Player.instance.gameObject)
		{
			Player.instance.state.TakeDamage(damage, gameObject, true);
		}
	}




}
