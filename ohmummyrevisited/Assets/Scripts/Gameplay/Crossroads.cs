using System;
using UnityEngine;

public class Crossroads : MonoBehaviour
{
	public static event EventHandler<Crossroads> OnAnyActivatedCrossroads;
	[SerializeField] private bool UseToSpawnAtReset = true;
    private bool activated;

	private void Awake()
	{
		Deactivate();
	}

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("enter trigger " + other.name);
		if(other.CompareTag("Player") && other.TryGetComponent(out TombWanderer tombWanderer))
		{
			Activate();
			tombWanderer.SetLastCrossroads(this);
		}
	}

	public bool GetUseToSpawnAtReset() => UseToSpawnAtReset;

	public void Deactivate()
	{
		activated = false;
	}

	public void Activate()
	{
		activated = true;
		OnAnyActivatedCrossroads?.Invoke(this, this);
	}

	public bool IsActive() => activated;
}
