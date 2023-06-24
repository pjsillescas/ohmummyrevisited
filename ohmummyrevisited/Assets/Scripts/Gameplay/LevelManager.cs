using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using StarterAssets;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance = null;

    [SerializeField] private CinemachineVirtualCamera TopVCam;
    [SerializeField] private CinemachineVirtualCamera ShoulderVCam;
    [SerializeField] private Door DoorEntrance;
    [SerializeField] private Door DoorExit;
    [SerializeField] private GameObject MummyPrefab;

    private int numMummies;
    private TombWanderer tombWanderer;
    private List<TombAdvanced> tombs;
    private bool keyUnlocked;
    private bool scrollUnlocked;
    private bool sarcophagusUnlocked;
    private ThirdPersonController controller;
    private DoorTest doorController;
    private List<Vector3> positions;
    private List<AIManager> mummies;
    private int score;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is another level manager");
            return;
        }
        
        Instance = this;
        numMummies = 1;
        mummies = new();
        score = 0;
    }

    public void IncrementMummies()
	{
        numMummies++;
	}

    public TombWanderer GetTombWanderer() => tombWanderer;

    // Start is called before the first frame update
    void Start()
    {
        tombs = new (FindObjectsByType<TombAdvanced>(FindObjectsSortMode.None));

        tombs.ForEach(tomb => tomb.OnOpenTomb += OnTombOpen);

        SwitchToShoulderView();
        ResetTombs();

        var player = GameObject.FindGameObjectWithTag("Player");
        tombWanderer = player.GetComponent<TombWanderer>();
        controller = player.GetComponent<ThirdPersonController>();
        doorController = FindObjectOfType<DoorTest>();
        DeactivateInput();

        positions = FindObjectsByType<Crossroads>(FindObjectsSortMode.None).ToList().Select(o => o.transform.position).ToList();
    }

    public void SwitchToTopView()
	{
        ShoulderVCam.enabled = false;
        TopVCam.enabled = true;
        //doorController.ActivateCollider();
    }

    public void DeactivateInput()
	{
        controller.SetUseInput(false);
	}
    public void ActivateInput()
    {
        controller.SetUseInput(true);
    }

    public void SwitchToShoulderView()
	{
        TopVCam.enabled = false;
        ShoulderVCam.enabled = true;
        if (doorController != null)
        {
            doorController.DeactivateCollider();
        }
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
        keyUnlocked = false;
        sarcophagusUnlocked = false;
        scrollUnlocked = false;

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
        tombs[shuffledList[4]].ResetTomb(TombAdvanced.TombType.mummy);

        for (int k1 = 5; k1 < 11;k1++)
		{
            tombs[shuffledList[k1]].SetTombType(TombAdvanced.TombType.chest);
		}
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
        bool previousCheck = keyUnlocked && sarcophagusUnlocked;
        switch (tomb.GetTombType())
		{
            case TombAdvanced.TombType.chest:
                ;
                break;
            case TombAdvanced.TombType.key:
                keyUnlocked = true;
                break;
            case TombAdvanced.TombType.sarcophagus:
                sarcophagusUnlocked = true;
                break;
            case TombAdvanced.TombType.scroll:
                scrollUnlocked = true;
                break;
            case TombAdvanced.TombType.none:
            default:
                break;
		}

        bool currentCheck = keyUnlocked && sarcophagusUnlocked;
        if (!previousCheck && currentCheck)
        {
            OpenExitDoor();
		}
	}

    private void SpawnMummies()
	{
        mummies.ForEach(mummy => Destroy(mummy.gameObject));
        mummies.Clear();

        var rand = new System.Random();
        for(var i=0;i<numMummies;i++)
		{
            var position = positions[rand.Next(positions.Count)];
            mummies.Add(Instantiate(MummyPrefab, position, Quaternion.identity).GetComponent<AIManager>());
		}
	}

    public void ResetGame()
	{
        ResetTombs();
        OpenEntranceDoor();
        SpawnMummies();
	}
}
