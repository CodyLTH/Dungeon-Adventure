using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class GateKeeperController2 : MonoBehaviour
{
	// Start is called before the first frame update
	public GameObject rockPrefab;
	public static int length;
	public GameObject[] HitBox = new GameObject[length];

	public float damage = 10f;

	Transform target;
	NavMeshAgent agent;
	Animator anim;
	string mode = "Move";


	public float maxDistance = 20;
	public float stopDistance = 2;
	public float shortRange = 2;
	public float longRange = 5;

	bool isDead = false;
	bool isAttacking = false;


	public float attackPeriod = 2f;
	public float lookPeriod = 2f;


	Quaternion rotation;
	float distance;
	float angle;
	float time;



	Vector3 throwDirection;

	void Start()
	{
		target = Player.instance.transform;
		agent = GetComponent<NavMeshAgent>();
		anim = GetComponentInChildren<Animator>();
		time = Time.time;
		mode = "Move";
	}

	bool playerFound = true;



	// Update is called once per frame
	void Update()
	{
		if (!playerFound || isDead)
		{
			time = Time.time;
			return;
		}

		distance = Vector3.Distance(target.position, transform.position);
		Vector3 targetDir = target.position - transform.position;
		targetDir.y = 0;
		angle = Vector3.Angle(targetDir, transform.forward);
		rotation = Quaternion.LookRotation(targetDir, Vector3.up);
		isAttacking = anim.GetCurrentAnimatorStateInfo(0).IsName("ShortAttack") || anim.GetCurrentAnimatorStateInfo(0).IsName("LongAttack");

		if (isAttacking)
		{
			return;
		}

		if(mode == "Move")
		{
			MoveMode();
		}
		else if (mode == "Attack")
		{
			AttackMode();
		}
		else if (mode == "Idle")
		{
			if (playerFound)
			{
				mode = "Move";
			}
		}


	}

	void MoveMode()
	{
		agent.isStopped = false;
		anim.SetFloat("Speed", 1);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2f);
		agent.SetDestination(target.position);

		if ((angle > 355 || angle < 10)&& distance < stopDistance)
		{
			mode = "Attack";
			time = Time.time;
		}
		if (Time.time - time > 5)
		{
			mode = "Attack";
			time = Time.time;
		}
	}

	void AttackMode()
	{
		agent.isStopped = true;
		anim.SetFloat("Speed", 0);
		if (angle >= 355 || angle <= 30)
		{
			time = Time.time;
			Attack();
		}
		if (!isAttacking && (Time.time - time > 3 || distance > longRange))
		{
			mode = "Move";
			time = Time.time;
		}
	}

	void Attack()
	{
		if (distance < shortRange)
		{
			anim.SetTrigger("ShortAttack");
		}
		else if (distance < longRange)
		{
			anim.SetTrigger("LongAttack");
		}
	}

	public void HitTriggerOn()
	{
		foreach(GameObject obj in HitBox)
		{
			obj.GetComponent<Collider>().enabled = true;
		}
	}
	public void HitTriggerOff()
	{
		foreach (GameObject obj in HitBox)
		{
			obj.GetComponent<Collider>().enabled = false;
		}
	}

	public void TakeDamageAnimation()
	{
		anim.Play("GetHit");
		mode = "Move";
	}

	public void DieAnimation()
	{
		anim.Play("Die");
		isDead = true;
	}


	public void ThrowRock()
	{
		GameObject obj = Instantiate(rockPrefab, rockPrefab.gameObject.transform.position, rockPrefab.gameObject.transform.rotation);
		obj.GetComponent<GateKeeperRock>().forward = transform.forward;
		obj.GetComponent<GateKeeperRock>().forward = transform.forward;
		obj.SetActive(true);

	}

	public void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere(transform.position, maxDistance);
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, stopDistance);
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, shortRange);
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, longRange);
	}













	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == Player.instance.gameObject && isAttacking)
		{
			Player.instance.state.TakeDamage(damage, gameObject, true);
		}
	}





}
