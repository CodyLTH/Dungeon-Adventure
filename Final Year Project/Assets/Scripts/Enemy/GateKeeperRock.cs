using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateKeeperRock : MonoBehaviour
{
	Rigidbody rb;

	public float damage;
	public float speed = 0.3f;
	public Vector3 forward;
	public Vector3 direction;
	float count = 0;
	bool flag = true;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		direction = Player.instance.transform.position - transform.position + Vector3.up;
		direction.Normalize();

		Vector3 normal = Vector3.Cross(forward, direction);
		float angle = Vector3.Angle(forward, direction);
		angle = Mathf.Clamp(angle, 0, 60);
		Vector3 dir = Quaternion.AngleAxis(angle, normal) * forward;
		rb.velocity = dir * speed;
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == Player.instance.gameObject && flag)
		{
			Player.instance.state.TakeDamage(damage, gameObject, false);
			flag = false;
			Invoke("DestroyObject", 0.8f);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject != Player.instance.gameObject)
		{
			flag = false;
		}
		Invoke("DestroyObject", 1.5f);
	}

	private void DestroyObject()
	{
		Destroy(gameObject);
	}
}
