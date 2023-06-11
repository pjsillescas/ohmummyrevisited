using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance = null;

    [SerializeField] private CinemachineVirtualCamera TopVCam;
    [SerializeField] private CinemachineVirtualCamera ShoulderVCam;

    private TombWanderer tombWanderer;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is another level manager");
            return;
        }
        Debug.Log("level manager created");
        Instance = this;

        tombWanderer = GameObject.FindGameObjectWithTag("Player").GetComponent<TombWanderer>();
    }

    public TombWanderer GetTombWanderer() => tombWanderer;

    // Start is called before the first frame update
    void Start()
    {
        SwitchToShoulderView();
    }

    public void SwitchToTopView()
	{
        ShoulderVCam.enabled = false;
        TopVCam.enabled = true;
    }

    public void SwitchToShoulderView()
	{
        TopVCam.enabled = false;
        ShoulderVCam.enabled = true;
    }
}
