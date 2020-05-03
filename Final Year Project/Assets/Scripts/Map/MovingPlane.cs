using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlane : MonoBehaviour
{
	GameObject player;
	CharacterController characterController;
	Animator anim;
	Rigidbody body;
	public Animator anim2;


	void Start()
    {
		player = Player.instance.gameObject;
		characterController = player.GetComponent<CharacterController>();
		anim = GetComponentInParent<Animator>();
		body = GetComponent<Rigidbody>();
	}


	private void OnTriggerStay(Collider other)
	{
		if(other.gameObject == player)
		{
			characterController.Move(body.velocity * Time.deltaTime);
			anim.SetBool("IsMoving", true);
			anim2.SetBool("IsMoving", true);
		}
	}


	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject == player)
		{
			player.transform.parent = null;
		}
	}

	
}
