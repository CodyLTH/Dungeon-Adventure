using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyController : MonoBehaviour
{
	// Start is called before the first frame update


	[SerializeField] protected float damage = 100f;
	[SerializeField] protected float lookBackTime = 3f;
	[SerializeField] protected float turnSmoothTime = 3;
	[SerializeField] protected float maxMovingDistance = 20;
	[SerializeField] protected float lookDistance = 10;
	[SerializeField] protected float stopDistance = 3;
	[SerializeField] protected float shortRange = 2;
	[SerializeField] protected float longRange = 4;


	float agentSpeed;
	float agentAccel;


	protected Transform target;
	protected NavMeshAgent agent;
	protected Animator anim;

	public bool playerFound = false;

	string mode = "Idle";

	bool isDead = false;
	bool isAttacking = false;
	bool isAnimation = false;


	Quaternion rotation;
	float time;
	protected float distance;
	protected float angle;
	protected bool willKnockDown = false;
	bool OutOfRange;

	Vector3 startPoint;
	Quaternion startRotation;



	protected void Start()
	{
		target = Player.instance.transform;
		agent = GetComponent<NavMeshAgent>();
		anim = GetComponentInChildren<Animator>();
		time = Time.time;
		playerFound = false;
		mode = "Idle";
		startPoint = transform.position;
		startRotation = transform.rotation;
		agent.stoppingDistance = stopDistance;
	}

	 void Update()
	{
		if (isDead)
		{
			time = Time.time;
			return;
		}
	
		distance = Vector3.Distance(target.position, transform.position);
		Vector3 targetDir = target.position - transform.position;
		targetDir.y = 0;
		targetDir.Normalize();
		angle = Vector3.SignedAngle(targetDir, transform.forward, Vector3.up);
		rotation = Quaternion.LookRotation(targetDir, Vector3.up);
		isAttacking = anim.GetCurrentAnimatorStateInfo(0).IsName("ShortAttack") || anim.GetCurrentAnimatorStateInfo(0).IsName("LongAttack");
		isAnimation = anim.GetCurrentAnimatorStateInfo(0).IsName("GetHit") || anim.GetCurrentAnimatorStateInfo(0).IsName("Die") || anim.GetCurrentAnimatorStateInfo(0).IsName("Found");

		if (mode != "Idle" && (Vector3.Distance(target.position, startPoint) > maxMovingDistance || Player.instance.state.isDead))
		{
			mode = "Return";
			ReturnMode();
			return;
		}
		if (isAttacking || isAnimation)
		{
			agent.isStopped = true;
			return;
		}
		if (mode == "Move")
		{
			MoveMode();
		}
		else if (mode == "Attack")
		{
			AttackMode();
		}
		else if (mode == "Idle")
		{
			IdleMode();
		}
		else if (mode == "Return")
		{
			ReturnMode();
		}
	}

	void IdleMode()
	{
		agent.isStopped = true;
		anim.SetFloat("Speed", 0);
		agent.SetDestination(target.position);
		if (distance < lookDistance && Mathf.Abs(angle) < 90)
		{
			playerFound = true;
		}
		if (playerFound)
		{
			anim.Play("Found");
			mode = "Move";
			time = Time.time;
		}
	}

	void MoveMode()
	{
		agent.isStopped = false;
		anim.SetFloat("Speed", 1);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnSmoothTime);
		agent.SetDestination(target.position);
		if (!isAttacking && !isAnimation && inAttackArea() && distance <= stopDistance)
		{
			mode = "Attack";
			time = Time.time;
		}
	}


	void AttackMode()
	{
		agent.isStopped = true;
		anim.SetFloat("Speed", 0);
		if (!isAttacking && inAttackArea() && distance <= longRange)
		{
			time = Time.time;
			Attack();
		}
		if (!isAttacking && !isAnimation && (Time.time - time > lookBackTime || distance > longRange))
		{
			mode = "Move";
			time = Time.time;
		}
	}

	public virtual void Attack()
	{
		if (distance < shortRange)
		{
			anim.SetTrigger("ShortAttack");
			anim.ResetTrigger("LongAttack");
			willKnockDown = false;
		}
		else if (distance < longRange)
		{
			anim.SetTrigger("LongAttack");
			anim.ResetTrigger("ShortAttack");
			willKnockDown = true;
		}
	}

	void ReturnMode()
	{
		agent.isStopped = false;
		if (Player.instance.state.isDead)
		{
			restart();
			return;
		}
		Vector3 targetDir = startPoint - transform.position;
		targetDir.y = 0;
		targetDir.Normalize();
		if (targetDir != Vector3.zero)
		{
			rotation = Quaternion.LookRotation(targetDir, Vector3.up);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnSmoothTime * 4);
		}
		if (agent.stoppingDistance > 0)
		{
			agent.SetDestination(startPoint);
			anim.SetFloat("Speed", 1);
			agentSpeed = agent.speed;
			agentAccel = agent.acceleration;
			agent.speed = 10f;
			agent.acceleration = 20f;
			agent.stoppingDistance = 0f;
		}
		else if (Vector3.Distance(transform.position, startPoint) < 0.2f)
		{
			agent.velocity = Vector3.zero;
			agent.speed = agentSpeed;
			agent.stoppingDistance = stopDistance;
			agent.SetDestination(target.position);
			restart();
		}
	}

	void restart()
	{
		anim.SetFloat("Speed", 0);
		playerFound = false;
		time = Time.time;
		transform.position = startPoint;
		transform.rotation = startRotation;
		GetComponent<EnemyState>().currentHealth = GetComponent<EnemyState>().health;
		mode = "Idle";
	}



	public virtual bool inAttackArea()
	{
		if (angle > 30)
		{
			return false;
		}
		return true;
	}

	public void TakeDamageAnimation()
	{
		agent.isStopped = true;
		anim.Play("GetHit");
		mode = "Move";
	}

	public void DieAnimation()
	{
		agent.isStopped = true;
		anim.Play("Die");
		isDead = true;
	}

	public virtual void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.white;
		if (playerFound)
		{
			Gizmos.DrawWireSphere(startPoint, maxMovingDistance);
		}
		else
		{
			Gizmos.DrawWireSphere(transform.position, maxMovingDistance);
		}
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(transform.position, lookDistance);
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, stopDistance);
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, shortRange);
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, longRange);
	}

	private void OnTriggerEnter(Collider other)
	{
		isAttacking = anim.GetCurrentAnimatorStateInfo(0).IsName("ShortAttack") || anim.GetCurrentAnimatorStateInfo(0).IsName("LongAttack");
		if (other.gameObject == Player.instance.gameObject && isAttacking)
		{
			Player.instance.state.TakeDamage(damage, gameObject, willKnockDown);
			Debug.Log(damage);
		}
	}





}
