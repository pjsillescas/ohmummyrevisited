using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigureTombs : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		LevelManager.Instance.ResetTombs();
	}
}
