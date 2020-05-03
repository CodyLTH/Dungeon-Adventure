using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unused : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		/*
		MeshRenderer[] playerModelMesh = player.GetComponentsInChildren<MeshRenderer>();
		SkinnedMeshRenderer[] playerModelMesh2 = player.GetComponentsInChildren<SkinnedMeshRenderer>();
		foreach (MeshRenderer obj in playerModelMesh)
		{
			obj.material.color = new Color(1f, 1f, 1f, 1f);
		}
		foreach (SkinnedMeshRenderer obj in playerModelMesh2)
		{
			obj.material.color = new Color(1f, 1f, 1f, 1f);
		}
		*/

		//Debug.Log("Player Take Damage");
		//Debug.Log("Take " + amount + " damage");
		//Debug.Log("Current HP = " + currentHealth);


	}

	/*
	void EdgeDectect()
	{
		Vector3 MoveDir;
		camRay.origin = transform.position + new Vector3(0f, 0.5f, 0f);
		camRay.direction = -transform.up;
		if (!Physics.Raycast(camRay, 1, collisionMask) && controller.isGrounded)
		{
			if (!Physics.Raycast(camRay.origin, camRay.direction + transform.forward, 1))
			{
				MoveDir = transform.forward * 10f;
			}
			else if (!Physics.Raycast(transform.position, camRay.direction - transform.forward, 1))
			{
				MoveDir = transform.forward * -10f;
			}
			else if (!Physics.Raycast(transform.position, camRay.direction + transform.right, 1))
			{
				MoveDir = transform.right * 10f;
			}
			else if (!Physics.Raycast(transform.position, camRay.direction - transform.right, 1))
			{
				MoveDir = transform.right * -10f;
			}
			else
			{
				MoveDir = transform.forward * -10f;
			}
			controller.SimpleMove(MoveDir);
		}
		Debug.DrawLine(camRay.origin, camRay.origin + camRay.direction + transform.forward, Color.red);
		Debug.DrawLine(camRay.origin, camRay.origin + camRay.direction - transform.forward, Color.blue);
		Debug.DrawLine(camRay.origin, camRay.origin + camRay.direction + transform.right, Color.green);
		Debug.DrawLine(camRay.origin, camRay.origin + camRay.direction - transform.right, Color.white);
		Debug.DrawLine(camRay.origin + transform.forward, camRay.origin + transform.forward + camRay.direction, Color.red);
	}


	void UpdateTmp(Vector3 vec)
	{
		tmp.Add(vec);
		if (tmp.Count > 10)
		{
			tmp.RemoveAt(0);
		}
	}

	Vector3 AverageTmp()
	{
		Vector3 sum = Vector3.zero;
		if (tmp.Count == 0)
		{
			return Vector3.zero;
		}
		foreach (Vector3 vec in tmp)
		{
			sum += vec;
		}
		Debug.Log(sum / tmp.Count);
		return sum / tmp.Count;
	}

	*/
	// Update is called once per frame
	void Update()
    {
        
    }
}
