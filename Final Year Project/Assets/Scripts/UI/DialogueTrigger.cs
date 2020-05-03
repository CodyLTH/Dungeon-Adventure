using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
	// Start is called before the first frame update
	public GameObject canvas;
	public static int size = 0;
	public string[] dialogue = new string[size];
	bool triggered = false;

	private void Start()
	{
		canvas = GameObject.Find("Canvas");
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == Player.instance.gameObject && !triggered)
		{
			canvas.GetComponent<Dialogue>().DisplayDialogue(dialogue);
			triggered = true;
		}
	}
}
