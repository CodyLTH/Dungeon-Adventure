using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class GateEnemyTrigger : MonoBehaviour
{
	public static int enemyCount = 1;
	public GameObject[] enemies = new GameObject[enemyCount];
	
	Animator anim;
	AudioSource sound;
	NavMeshObstacle obstacle;
	bool opened;


	// Start is called before the first frame update
	void Start()
    {
		opened = false;
		obstacle = GetComponentInParent<NavMeshObstacle>();
		anim = GetComponentInParent<Animator>();
		sound = GetComponentInParent<AudioSource>();
	}



    // Update is called once per frame
    void Update()
    {
		if (opened)
		{
			return;
		}
		foreach (GameObject obj  in enemies)
		{
			if(obj != null)
			{
				return;
			}
		}
		opened = true;
		GateOpen();
	}


	void GateOpen()
	{
		anim.SetBool("Open", true);
		if(obstacle != null) obstacle.enabled = false;
		sound.Play();
	}
}
