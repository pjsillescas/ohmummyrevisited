using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orientator : MonoBehaviour
{
    [SerializeField] private float Orientation = 0f;

	private void OnTriggerEnter(Collider other)
	{
        other.transform.rotation = Quaternion.AngleAxis(Orientation, Vector3.up);
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
