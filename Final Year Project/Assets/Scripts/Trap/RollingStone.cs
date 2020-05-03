using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingStone : MonoBehaviour
{
	Rigidbody rb;

	public float damage;
	public float speed = 0.3f;
	Vector3 direction;


	void Start()
	{
		direction = transform.forward;
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		rb.AddForce(direction * speed);
	}


	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == Player.instance.gameObject)
		{
			Player.instance.state.TakeDamage(damage, gameObject, true);

		}
		else if (other.gameObject.CompareTag("Outside"))
		{
			Destroy(gameObject);
		}
	}

}
