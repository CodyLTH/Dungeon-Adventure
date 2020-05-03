using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
	public GameObject mainCamera;
	public GameObject virtualCamera;
	PlayerController playerController;
	GameObject player;

	// Start is called before the first frame update
	void Start()
    {
		mainCamera = Camera.main.gameObject;
		player = gameObject;
		playerController = gameObject.GetComponent<PlayerController>();
	}

	// Update is called once per frame
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			virtualCamera.SetActive(true);
			mainCamera.SetActive(false);
			Debug.Log(virtualCamera.transform);
			//playerController.cameraT = virtualCamera.transform;
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			virtualCamera.SetActive(false);
			mainCamera.SetActive(true);
			//playerController.cameraT = mainCamera.transform;
		}
	}
}
