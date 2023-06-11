using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewSwitcher : MonoBehaviour
{
    public enum ViewType { top, shoulder }

    [SerializeField] private ViewType View;

	private void OnTriggerEnter(Collider other)
	{
        if (View == ViewType.top)
        {
            LevelManager.Instance.SwitchToTopView();
        }
        else
		{
            LevelManager.Instance.SwitchToShoulderView();
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
