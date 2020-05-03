using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class GateKeeperController : EnemyController
{

	public GameObject rockPrefab;

	public override void Attack()
	{
		if (distance < shortRange)
		{
			anim.SetTrigger("ShortAttack");
			willKnockDown = true;
		}
		else if (distance < longRange)
		{
			anim.SetTrigger("LongAttack");
			willKnockDown = true;
		}
	}

	public override bool inAttackArea()
	{
		if (angle > 40 || angle < -10)
		{
			return false;
		}
		return true;
	}




	public void ThrowRock()
	{
		GameObject obj = Instantiate(rockPrefab, rockPrefab.gameObject.transform.position, rockPrefab.gameObject.transform.rotation);
		obj.GetComponent<GateKeeperRock>().forward = transform.forward;
		obj.GetComponent<GateKeeperRock>().forward = transform.forward;
		obj.SetActive(true);

	}




}
