using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{

	public float speed = 0.3f;

	// Start is called before the first frame update
	void Start()
    {
		Invoke("Destroy", 4f);
	}

    // Update is called once per frame
    void Update()
    {
		transform.Translate(Vector3.back * speed);
	}


	void Destroy()
	{
		Destroy(gameObject);
	}

	private void OnTriggerEnter(Collider other)
	{
	}



}
