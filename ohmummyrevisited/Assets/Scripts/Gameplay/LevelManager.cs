using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance = null;

    [SerializeField] private CinemachineVirtualCamera TopVCam;
    [SerializeField] private CinemachineVirtualCamera ShoulderVCam;
    [SerializeField] private Door DoorEntrance;
    [SerializeField] private Door DoorExit;


    private TombWanderer tombWanderer;
    private List<TombAdvanced> tombs;
    private int numTombs;

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
        tombs = new (FindObjectsByType<TombAdvanced>(FindObjectsSortMode.None));

        tombs.ForEach(tomb => tomb.OnOpenTomb += OnTombOpen);

        SwitchToShoulderView();
        ResetTombs();
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

    public static void Shuffle(List<int> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0,list.Count);
            int value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public void ResetTombs()
	{
        Debug.Log("reset");

        foreach (var tomb in tombs)
        {
            tomb.ResetTomb(TombAdvanced.TombType.none);
            tomb.GetComponentInChildren<MeshRenderer>().material.color = Color.grey;
        }


        int[] ids = new int[tombs.Count];
        for(int k=0;k< tombs.Count;k++)
		{
            ids[k] = k;
		}

        List<int> shuffledList = (new List<int>(ids));
        Shuffle(shuffledList);

        tombs[shuffledList[0]].ResetTomb(TombAdvanced.TombType.scroll);
        tombs[shuffledList[1]].ResetTomb(TombAdvanced.TombType.chest);
        tombs[shuffledList[2]].ResetTomb(TombAdvanced.TombType.key);
        tombs[shuffledList[3]].ResetTomb(TombAdvanced.TombType.sarcophagus);

        for(int k1 = 4; k1 < 10;k1++)
		{
            tombs[shuffledList[k1]].SetTombType(TombAdvanced.TombType.chest);
		}

        numTombs = tombs.Count;
	}

    public void OpenEntranceDoor()
	{
        DoorEntrance.Open();
        DoorExit.Close();
    }

    public void OpenExitDoor()
    {
        DoorEntrance.Close();
        DoorExit.Open();
    }

    private void OnTombOpen(object sender, TombAdvanced tomb)
	{
        numTombs--;

        if(numTombs <= 0)
		{
            OpenExitDoor();
		}
	}
}
