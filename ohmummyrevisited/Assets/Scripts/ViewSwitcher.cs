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
                LevelManager.Instance.ActivateInput();
                break;
            case ViewType.shoulder:
            default:
                LevelManager.Instance.SwitchToShoulderView();
                LevelManager.Instance.DeactivateInput();
                break;
		}
    }
}
