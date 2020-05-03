using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateStone : MonoBehaviour
{
	public GameObject stonePrefab;
	public float period = 2f;

	// Start is called before the first frame update
	void Start()
    {
		InvokeRepeating("Generate", period, period);
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	void Generate()
	{
		GameObject prefab = Instantiate(stonePrefab, transform.position, transform.rotation);
		prefab.transform.SetParent(gameObject.transform);
	}


}
