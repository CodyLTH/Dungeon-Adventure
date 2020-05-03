using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateButtonTrigger : MonoBehaviour
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
		meshRenderer.material.SetColor("_EmissionColor", Color.red);
	}

	void GateOpen()
	{
		if (!opened)
		{
			opened = true;
			anim.SetBool("Open", true);
			sound.Play();
			meshRenderer.material.color = Color.green;
			meshRenderer.material.SetColor("_EmissionColor", Color.green);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		GateOpen();
	}




}
