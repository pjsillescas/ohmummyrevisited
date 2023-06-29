using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orientator : MonoBehaviour
{
    [SerializeField] private float Orientation = 0f;
    [SerializeField] private Transform TargetTransform;
    [SerializeField] private float Duration;
    [SerializeField] private bool DoJustRotation = false;

    float time;
    bool isActive;
    Transform initialTransform;
    GameObject rotatee;

	private void OnTriggerEnter(Collider other)
	{
        if (DoJustRotation)
        {
            other.transform.rotation = Quaternion.AngleAxis(Orientation, Vector3.up);
        }
        else
		{
            time = 0;
            initialTransform = transform;
            rotatee = other.gameObject;
            isActive = true;
            rotatee.GetComponent<ThirdPersonController>().UseMovement(false);
        }
    }

	// Start is called before the first frame update
	void Start()
    {
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive)
		{
            time += Time.deltaTime;
            var alpha = time / Duration;
            if (Vector3.SqrMagnitude(rotatee.transform.position - TargetTransform.position) > 0.1f)
            {
                rotatee.transform.LookAt(TargetTransform);
                rotatee.transform.position = Vector3.Slerp(initialTransform.position, TargetTransform.position, alpha);
            }
            else
			{
                isActive = false;
                Debug.Log("before " + rotatee.transform.forward);
                //rotatee.transform.rotation.SetLookRotation(TargetTransform.forward, TargetTransform.up);
                rotatee.transform.rotation = Quaternion.Euler(TargetTransform.rotation.eulerAngles);
                Debug.Log("after " + rotatee.transform.forward);
                rotatee.GetComponent<ThirdPersonController>().UseMovement(true);
                Debug.Log(TargetTransform.forward);
            }

            Debug.Log(rotatee.transform.rotation.y);
		}
    }
}
