using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CameraController : MonoBehaviour
{
	//Target
	public Transform target;

	//MouseSetting
	public bool lockCursor;
	public float mouseSensitivity = 10;
	public float scrollSensitivity = 10;

	//Camera Setting
	public float dstFromTarget = 5f;
	public Vector2 dstMinMax = new Vector2(4, 10);
	public Vector2 pitchMinMax = new Vector2(-30, 85);
	public float rotationSmoothTime = .12f;
	public float positionSmoothSpeed = 1f;

	//Variable
	Vector3 rotationSmoothVelocity;
	Vector3 currentRotation;
	public float yaw;
	public float pitch;

	//Collision
	public float collisionCushion = 0f;
	public LayerMask collisionMask;
	public Ray[] clipPoints = new Ray[5];
	float adjustedDst;
	public float minAdjustedDst = 0.5f;


	//Debug
	public bool collision = true;

	void Start()
	{
		target = Player.instance.gameObject.transform.Find("Target");
		adjustedDst = dstFromTarget;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	void LateUpdate()
	{
		if(Time.timeScale == 0 || Cursor.visible)
		{
			yaw = currentRotation.y;
			pitch = currentRotation.x;
			CameraCollision();
			return;
		}
		dstFromTarget -= Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity;
		dstFromTarget = Mathf.Clamp(dstFromTarget, dstMinMax.x, dstMinMax.y);
		yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
		pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
		pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y); ;
		
		currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
		transform.eulerAngles = currentRotation;

		CameraCollision();

	}

	void CameraCollision()
	{
		RaycastHit RayHit;
		Vector3 desiredPosition;
		clipPoints[0] = new Ray(target.transform.position, transform.position - target.transform.position);
		clipPoints[1] = new Ray(target.transform.position, transform.position + transform.up * 0.2f - target.transform.position);
		clipPoints[2] = new Ray(target.transform.position, transform.position - transform.up * 0.2f - target.transform.position);
		clipPoints[3] = new Ray(target.transform.position, transform.position + transform.right * 0.35f - target.transform.position);
		clipPoints[4] = new Ray(target.transform.position, transform.position - transform.right * 0.35f - target.transform.position);

		adjustedDst = dstFromTarget;
		foreach (Ray ray in clipPoints)
		{
			if (Physics.Raycast(ray, out RayHit, dstFromTarget, collisionMask))
			{
				float distance = RayHit.distance - collisionCushion;
				if (distance < adjustedDst && collision)
				{
					adjustedDst = distance;
				}
			}
			Debug.DrawLine(ray.origin, ray.origin + ray.direction * adjustedDst, Color.red);
		}
		adjustedDst = Mathf.Max(adjustedDst, minAdjustedDst);
		desiredPosition = target.position - transform.forward * adjustedDst;
		transform.position = Vector3.Lerp(transform.position, desiredPosition, positionSmoothSpeed);
	}



}
