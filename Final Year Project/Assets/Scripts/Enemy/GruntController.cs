using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntController : EnemyController
{
	public override void Attack()
	{
		if (Mathf.Abs(angle) < 10)
		{
			anim.SetTrigger("ShortAttack");
			willKnockDown = false;
		}
		else
		{
			anim.SetTrigger("LongAttack");
			willKnockDown = true;
		}
	}

	public override bool inAttackArea()
	{
		if (Mathf.Abs(angle) > 25)
		{
			return false;
		}
		return true;
	}
}