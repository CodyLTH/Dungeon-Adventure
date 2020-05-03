using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
	bool flag = true;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == Player.instance.gameObject && flag)
		{
			Debug.Log("Update Respawn Point");
			Player.instance.state.respawnPoint = gameObject.transform;
			flag = false;
		}
	}
}
