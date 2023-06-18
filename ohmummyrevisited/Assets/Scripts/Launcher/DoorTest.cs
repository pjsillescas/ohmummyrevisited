using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTest : MonoBehaviour
{
    public Door door;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
        if (door != null)
        {
            Debug.Log("open");
            door.Open();
        }
        else
        {
            Debug.Log("open: door is null");
        }
    }

    private void OnTriggerExit(Collider other)
	{
        if (door != null)
        {
            Debug.Log("close");
            door.Close();
        }
        else
		{
            Debug.Log("close: door is null");
		}
	}
}
