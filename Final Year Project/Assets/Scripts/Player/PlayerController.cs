using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	//Components
	Transform cameraT;
	Animator anim;
	CharacterController controller;
	//Settings
	public float walkSpeed = 3;
	public float runSpeed = 8;
	public float turnSmoothTime = 0.05f;
	public float gravity = -30f;
	public float jumpHeight = 5;
	public float speedSmoothTime = 0.1f;
	public float airControlPercent;
	//Variable
	float velocityY = 0;
	float turnSmoothVelocity;
	float speedSmoothVelocity;
	float currentSpeed;
	Vector2 inputDir;
	//States
	bool walking;
	public bool isAttacking = false;
	bool isMainState = false;
	bool isKnockedDown = false;
	bool isGettingHit = false;
	bool controllable = true;
	//Edge Detection
	Ray groundRay;
	Ray edgeRay;
	Vector3 edgeDetectMoveDir;
	public LayerMask collisionMask;

	void Start()
	{
		cameraT = Camera.main.transform;
		anim = GetComponentInChildren<Animator>();
		controller = GetComponent<CharacterController>();
	}
	void Update()
	{
		Vector3 velocity = transform.forward * currentSpeed + Vector3.up * velocityY;
		controller.Move(velocity * Time.deltaTime);
		GetCurrentState();

		if (controllable)
		{
			Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
			inputDir = input.normalized;
			walking = Input.GetKey(KeyCode.LeftShift);
		}

		EdgeDectect();
		Movement(inputDir, walking);

		if (!controller.isGrounded)
		{
			edgeDetectMoveDir = Vector3.zero;
			if (!Physics.Raycast(groundRay, 1.5f, collisionMask))
			{
				anim.SetBool("IsGrounded", false);
			}
			velocityY += Time.deltaTime * gravity;
			return;
		}

		velocityY = -3f;

		if (Physics.Raycast(groundRay, 0.8f, collisionMask))
		{
			anim.SetBool("IsGrounded", true);
		}

		if (controllable)
		{
			if (Input.GetMouseButtonDown(0))
			{
				Attack(inputDir);
			}
			if (Input.GetKeyDown(KeyCode.Space))
			{
				Jump();
			}
		}
		else
		{
			currentSpeed = 0;
			velocityY = -3f;
		}
	}

	void Movement(Vector2 inputDir, bool walking)
	{
		if (Cursor.visible)
		{
			currentSpeed = Mathf.SmoothDamp(currentSpeed, 0, ref speedSmoothVelocity, speedSmoothTime);
			anim.SetFloat("Speed", 0);
			return;
		}

		if (Time.timeScale == 0 || Player.instance.state.isDead || !controllable)
		{
			return;
		}
		
		float targetSpeed = ((walking) ? walkSpeed : runSpeed) * inputDir.magnitude;
		currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);
		float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;

		if (inputDir != Vector2.zero)
		{
			transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, GetModifiedSmoothTime(turnSmoothTime));
		}
		anim.SetFloat("Speed", targetSpeed*2/ runSpeed);
	}
	void Jump()
	{
		if (Time.timeScale == 0 || Player.instance.state.isDead || Cursor.visible || !controllable)
		{
			return;
		}
		float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);
		velocityY = jumpVelocity;
		anim.Play("JumpStart");
	}
	void Attack(Vector2 inputDir)
	{
		if (Time.timeScale == 0 || Player.instance.state.isDead || !controllable || Cursor.visible)
		{
			return;
		}
		anim.SetFloat("Speed", 0);

		if (Input.GetMouseButton(1))
		{
			transform.eulerAngles = Vector3.up * cameraT.eulerAngles.y;
			currentSpeed = 0;
			anim.Play("NormalAttack01");
		}
		else if (inputDir != Vector2.zero)
		{
			float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
			transform.eulerAngles = Vector3.up * targetRotation;
			currentSpeed = 2;
			anim.Play("NormalAttack02");
		}
		else
		{
			currentSpeed = 0;
			anim.Play("NormalAttack01");
		}
	}

	public void GetHitAnimation()
	{
		currentSpeed = 0;
		anim.Play("GetHit");
	}
	public void KnockedDownAnimation()
	{
		anim.Play("KnockedDown");
		velocityY = 5f;
		currentSpeed = -10f;
	}
	public void DieAnimation()
	{
		anim.Play("Die");
	}

	float GetModifiedSmoothTime(float smoothTime)
	{
		if (controller.isGrounded)
		{
			return smoothTime;
		}

		if (airControlPercent == 0)
		{
			return float.MaxValue;
		}
		return smoothTime / airControlPercent;
	}
	public void ResetCurrentSpeed()
	{
		if (inputDir == Vector2.zero)
		{
			currentSpeed = 0;
		}
	}

	void GetCurrentState()
	{
		isAttacking = anim.GetCurrentAnimatorStateInfo(0).IsName("NormalAttack01") || anim.GetCurrentAnimatorStateInfo(0).IsName("NormalAttack02");
		isKnockedDown = anim.GetCurrentAnimatorStateInfo(0).IsName("KnockedDown") || anim.GetCurrentAnimatorStateInfo(0).IsName("Recover");
		isGettingHit = anim.GetCurrentAnimatorStateInfo(0).IsName("GetHit");
		controllable = !(isAttacking || isKnockedDown || isGettingHit);
	}

	void EdgeDectect()
	{
		groundRay.origin = transform.position + new Vector3(0f, 0.5f, 0f);
		groundRay.direction = -transform.up;
		if (!Physics.Raycast(groundRay, 0.8f, collisionMask) && controller.isGrounded)
		{
			controller.SimpleMove(edgeDetectMoveDir * 20f);
		}
	}

	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (!Physics.Raycast(groundRay, 0.8f, collisionMask) || controller.isGrounded)
		{
			edgeDetectMoveDir = Vector3.zero;
			float rayLength = 1.5f;
			edgeRay.origin = transform.position + Vector3.up * 0.2f;
			edgeRay.direction = transform.forward - Vector3.up * 0.1f;
			for (int i = 0; i < 12; i++)
			{
				edgeRay.direction = Quaternion.AngleAxis(30, Vector3.up) * edgeRay.direction;
				if (Physics.Raycast(edgeRay, rayLength))
				{
					edgeDetectMoveDir -= new Vector3(edgeRay.direction.x, 0, edgeRay.direction.z);
					Debug.DrawLine(edgeRay.origin, edgeRay.origin + edgeRay.direction * rayLength, Color.green);
				}
				else
				{
					Debug.DrawLine(edgeRay.origin, edgeRay.origin + edgeRay.direction * rayLength, Color.red);
				}
			}
			if(edgeDetectMoveDir == Vector3.zero)
			{
				edgeDetectMoveDir = transform.forward;
			}
			edgeDetectMoveDir.Normalize();
		}
	}
}

