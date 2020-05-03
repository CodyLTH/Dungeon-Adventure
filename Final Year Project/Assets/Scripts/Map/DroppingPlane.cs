using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppingPlane : MonoBehaviour
{
	Animator anim;
	void Start()
	{
		anim = GetComponentInParent<Animator>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == Player.instance.gameObject)
		{
			anim.SetTrigger("Drop");
		}
	}
}
