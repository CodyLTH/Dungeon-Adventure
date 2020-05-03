using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomTrigger : MonoBehaviour
{
	public GateKeyTrigger script;
	public static int enemyNumber;
	public EnemyController[] enemies = new EnemyController[enemyNumber];

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == Player.instance.gameObject)
		{
			if (script != null)
			{
				script.GateClose();
			}
			foreach(EnemyController ec in enemies)
			{
				ec.playerFound = true;
			}
		}
	}
}
