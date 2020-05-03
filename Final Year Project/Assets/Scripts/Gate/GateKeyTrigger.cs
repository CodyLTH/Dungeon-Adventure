using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GateKeyTrigger : MonoBehaviour
{
	Animator anim;
	AudioSource sound;
	bool opened;
	MeshRenderer meshRenderer;

	// Start is called before the first frame update
	void Start()
	{
		opened = false;
		anim = GetComponentInParent<Animator>();
		sound = GetComponentInParent<AudioSource>();
		meshRenderer = GetComponentInParent<MeshRenderer>();
		meshRenderer.material.color = Color.red;
		meshRenderer.material.SetColor("_EmissionColor", Color.red * 1f);
	}

	void GateOpen()
	{
		if (!opened)
		{
			opened = true;
			anim.SetBool("Open", true);
			sound.Play();
			meshRenderer.material.color = Color.green;
			meshRenderer.material.SetColor("_EmissionColor", Color.green * 1f);
		}

	}
	public void GateClose()
	{
		if (opened)
		{
			opened = false;
			anim.SetBool("Open", false);
			meshRenderer.material.color = Color.red;
			meshRenderer.material.SetColor("_EmissionColor", Color.red * 1f);
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject == Player.instance.gameObject)
		{
			if (Input.GetKey(KeyCode.E))
			{
				GateOpen();
			}
		}
	}

	

}
