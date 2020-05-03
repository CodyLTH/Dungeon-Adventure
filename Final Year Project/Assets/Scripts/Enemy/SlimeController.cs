using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : EnemyController
{
	public float shortDamage;
	public float longDamage;

	public override void Attack()
	{
		if (distance < shortRange)
		{
			damage = shortDamage;
			anim.SetTrigger("ShortAttack");
			willKnockDown = false;
		}
		else if (distance < longRange)
		{
			damage = longDamage;
			anim.SetTrigger("LongAttack");
			willKnockDown = true;
		}
	}

	public override bool inAttackArea()
	{
		if (Mathf.Abs(angle) > 20)
		{
			return false;
		}
		return true;
	}
}
