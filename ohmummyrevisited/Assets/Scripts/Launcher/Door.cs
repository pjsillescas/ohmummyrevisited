using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public enum DoorState { open, closed }

    private const float ANGLE_STEP = 2f;
    private const float TIME_STEP = 0.02f;

    [SerializeField] private MeshRenderer LeftBlade;
    [SerializeField] private MeshRenderer RightBlade;

    private DoorState status;

	// Start is called before the first frame update
	void Start()
    {
        status = DoorState.closed;
    }

    public void Open()
	{
        if(status == DoorState.closed)
		{
            status = DoorState.open;
            StartCoroutine(SwitchStatus());
		}
    }

    public void Close()
    {
        if (status == DoorState.open)
        {
            status = DoorState.closed;
            StartCoroutine(SwitchStatus());
        }
    }

    private IEnumerator SwitchStatus()
	{
        float angle = (status == DoorState.open) ? 0 : 90;
        float step = ANGLE_STEP;
        bool stop = false;
        while (!stop)
        {
            switch (status)
            {
                case DoorState.open:
                    angle += step;
                    stop = angle >= 90f;
                    break;
                case DoorState.closed:
                default:
                    angle -= step;
                    stop = angle <= 0;
                    break;
            }
            RightBlade.transform.rotation = Quaternion.Euler(-90f, 90 - angle, 0);
            LeftBlade.transform.rotation = Quaternion.Euler(-90f, angle - 90, 0);

            if (!stop)
            {
                yield return new WaitForSeconds(TIME_STEP);
            }
        }

        yield return null;
	}

}
