using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyContoller2 : MonoBehaviour
{
	// Start is called before the first frame update


	public float lookRadius = 10f;
	public float damage = 10f;
	public float shortAttackDistance = 2.5f;
	public float longAttackDistance = 4f;
	Transform target;
	NavMeshAgent agent;
	Collider modelCollider;
	Animator anim;


	bool isDead = false;
	bool shortAttack = false;
	bool isAttacking = false;
	bool attackFlag = false;
	bool lookState = false;

	public float attackPeriod = 2f;
	public float lookPeriod = 2f;
	float distance;

	void Start()
	{
		target = Player.instance.transform;
		agent = GetComponent<NavMeshAgent>();
		anim = GetComponentInChildren<Animator>();
		InvokeRepeating("ResetAttackFlag", 1f, 1f);
		InvokeRepeating("LookAtTarget", 0f, lookPeriod);
	}

	// Update is called once per frame
	void Update()
	{
		if (isDead)
		{
			return;
		}
		GetAttackState();
		distance = Vector3.Distance(target.position, transform.position);
		if (distance < lookRadius)
		{
			agent.SetDestination(target.position);

			if (lookState && !isAttacking)
			{
				var rotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
				transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2f);
			}

			if (distance <= longAttackDistance && !isAttacking && !attackFlag)
			{
				attackFlag = true;
				if(distance <= shortAttackDistance)
				{
					shortAttack = true;
				}
				else
				{
					shortAttack = false;
				}
				anim.SetBool("ShortAttack", shortAttack);
				anim.Play("Taunt");
			}
		}
	}


	void GetAttackState()
	{
		isAttacking = anim.GetCurrentAnimatorStateInfo(0).IsName("Attack02") || anim.GetCurrentAnimatorStateInfo(0).IsName("Attack01")|| anim.GetCurrentAnimatorStateInfo(0).IsName("Taunt");

	}

	void ResetAttackFlag() {
		int rand = Random.Range(0, 2);
		if (rand == 0)
		{
			attackFlag = false;
		}
	}

	public void TakeDamageAnimation()
	{
		anim.Play("GetHit");
		if (!isAttacking)
		{
			var direction = target.transform.position - transform.position; ;
			direction.y = 0;
			transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
		}
	}

	public void DieAnimation()
	{
		var direction = target.transform.position - transform.position; ;
		direction.y = 0;
		transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
		anim.Play("Die");
		isDead = true;
	}




	void LookAtTarget(){
		int rand = Random.Range(0, 2);
		if (lookState)
		{
			lookState = false;
		}
		else	if (rand == 0)
		{
			lookState = true;
		}
	}

	public void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, lookRadius);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == Player.instance.gameObject && isAttacking)
		{
			if (shortAttack)
			{
				Player.instance.state.TakeDamage(damage, gameObject, false);
			}
			else
			{
				Player.instance.state.TakeDamage(damage, gameObject, true);
			}
		}
	}





}
