using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{

	public GameObject dialogueUI;

	TextMeshProUGUI textMesh;
	string[] dialogue;
	int index;

	private void Start()
	{
		textMesh = dialogueUI.GetComponentInChildren<TextMeshProUGUI>();
	}

	public void DisplayDialogue(string[] d)
	{
		dialogue = d;
		if(dialogue.Length <= 0)
		{
			return;
		}
		dialogueUI.SetActive(true);
		index = 0;
		textMesh.text = dialogue[0];
		GetComponent<CursorLock>().ShowCursor();
	}

	public void NextDialogue()
	{
		index++;
		if (index < dialogue.Length)
		{
			textMesh.text = dialogue[index];
		}
		else
		{
			dialogueUI.SetActive(false);
			GetComponent<CursorLock>().LockCursor();
		}
	}




}
