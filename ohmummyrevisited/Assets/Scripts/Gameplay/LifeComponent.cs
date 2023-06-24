using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeComponent : MonoBehaviour
{
    public event EventHandler OnLifeOver;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("mummy"))
		{
            other.GetComponent<AIManager>().Die();

            if (LevelManager.Instance.IsScrollUnlocked())
            {
                Debug.Log("using scroll");
                LevelManager.Instance.ExhaustScroll();
            }
            else
            {
                OnLifeOver?.Invoke(this, EventArgs.Empty);
            }
		}
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
