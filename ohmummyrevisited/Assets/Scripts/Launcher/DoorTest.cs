using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTest : MonoBehaviour
{
    public enum ActionTime { onEnter, onExit, none }
    public enum DoorAction { open, close, none }

    [SerializeField] private Door SelectedDoor;
    [SerializeField] private ActionTime SelectedActionTime = ActionTime.none;
    [SerializeField] private DoorAction SelectedAction = DoorAction.none;

    private void PerformAction()
	{
        switch(SelectedAction)
		{
            case DoorAction.close:
                Debug.Log("close");
                SelectedDoor.Close();
                break;
            case DoorAction.open:
                Debug.Log("open");
                SelectedDoor.Open();
                break;
            case DoorAction.none:
            default:
                break;
		}

        GetComponent<BoxCollider>().isTrigger = false;
	}
	private void OnTriggerEnter(Collider other)
	{
        if (SelectedDoor != null && ActionTime.onEnter.Equals(SelectedActionTime))
        {
            PerformAction();
        }
    }

    private void OnTriggerExit(Collider other)
	{
        if (SelectedDoor != null && ActionTime.onExit.Equals(SelectedActionTime))
        {
            PerformAction();
        }
	}
}
