using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateAttackTrigger : MonoBehaviour
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
	}

	public void GateOpen()
	{
		if (!opened)
		{
			opened = true;
			anim.SetBool("Open", true);
			sound.Play();
			meshRenderer.material.color = Color.red;
			meshRenderer.material.SetColor("_EmissionColor", Color.red *3.5f);
		}

	}
}
