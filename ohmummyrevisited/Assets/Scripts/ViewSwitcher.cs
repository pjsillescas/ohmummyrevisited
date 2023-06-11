using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewSwitcher : MonoBehaviour
{
    public enum ViewType { top, shoulder }

    [SerializeField] private ViewType View;

	private void OnTriggerEnter(Collider other)
	{
        switch(View)
		{
            case ViewType.top:
                LevelManager.Instance.SwitchToTopView();
                break;
            case ViewType.shoulder:
            default:
                LevelManager.Instance.SwitchToShoulderView();
                break;
		}
    }
}
