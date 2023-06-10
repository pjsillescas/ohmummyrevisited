using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TombAdvanced : MonoBehaviour
{
    [SerializeField] private Transform NorthEast;
    [SerializeField] private Transform NorthWest;
    [SerializeField] private Transform SouthEast;
    [SerializeField] private Transform SouthWest;

    private Crossroads NorthEastCrossroads;
    private Crossroads NorthWestCrossroads;
    private Crossroads SouthEastCrossroads;
    private Crossroads SouthWestCrossroads;

    private bool north;
    private bool south;
    private bool east;
    private bool west;
    private bool open;
    private TombWanderer tombWanderer;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start tomb advanced " + name);
        tombWanderer = null;

        LoadCrossroads();

        Crossroads.OnAnyActivatedCrossroads += ActivateCrossroads;
        north = false;
        south = false;
        east = false;
        west = false;
        open = false;
    }

	private void OnDestroy()
	{
        Crossroads.OnAnyActivatedCrossroads -= ActivateCrossroads;
        Debug.Log("destroying " + name);
	}

	private void LoadCrossroads()
	{
        NorthEastCrossroads = GetCrossroads(NorthEast);
        NorthWestCrossroads = GetCrossroads(NorthWest);
        SouthEastCrossroads = GetCrossroads(SouthEast);
        SouthWestCrossroads = GetCrossroads(SouthWest);
    }

    private Crossroads GetCrossroads(Transform transform)
	{
        var origin = transform.position + new Vector3(0,-10,0);
        var direction = Vector3.up;
        var hits = Physics.RaycastAll(origin, direction, 10);

        Crossroads crossroads = null;
        foreach(var hit in hits)
		{
            crossroads = hit.collider.GetComponent<Crossroads>();
            if (crossroads != null)
			{
                return crossroads;
			}
		}

        Debug.Log("crossroads " + transform.name + " not found");
        return crossroads;
	}

    private Crossroads GetLastCrossroads()
    {
        if (tombWanderer == null)
        {
            tombWanderer = LevelManager.Instance.GetTombWanderer();
        }

        return tombWanderer.GetLastCrossroads();
    }

    private void ActivateCrossroads(object sender, Crossroads crossroads)
    {
        var lastCrossroads = GetLastCrossroads();
        if (NorthEastCrossroads == crossroads)
        {
            if (lastCrossroads == NorthWestCrossroads)
            {
                Debug.Log(name + " north");
                north = true;
            }
            else if (lastCrossroads == SouthEastCrossroads)
            {
                Debug.Log(name + " east");
                east = true;
            }
        }
        else if (SouthEastCrossroads == crossroads)
        {
            if (lastCrossroads == NorthEastCrossroads)
            {
                Debug.Log(name + " east");
                east = true;
            }
            else if (lastCrossroads == SouthWestCrossroads)
            {
                Debug.Log(name + " south");
                south = true;
            }
        }
        else if (NorthWestCrossroads == crossroads)
        {
            if (lastCrossroads == NorthEastCrossroads)
            {
                Debug.Log(name + " north");
                north = true;
            }
            else if (lastCrossroads == SouthWestCrossroads)
            {
                Debug.Log(name + " west");
                west = true;
            }
        }
        else if (SouthWestCrossroads == crossroads)
        {
            if (lastCrossroads == NorthWestCrossroads)
            {
                Debug.Log(name + " west");
                west = true;
            }
            else if (lastCrossroads == SouthEastCrossroads)
            {
                Debug.Log(name + " south");
                south = true;
            }
        }

        /*
        if(IsTuple(lastCrossroads,crossroads, NorthWestCrossroads, NorthEastCrossroads))
		{
            Debug.Log(name + " north");
            north = true;
		}
        else if (IsTuple(lastCrossroads, crossroads, SouthEastCrossroads, NorthEastCrossroads))
        {
            Debug.Log(name + " east");
            east = true;
        }
        else if (IsTuple(lastCrossroads, crossroads, SouthEastCrossroads, SouthWestCrossroads))
        {
            Debug.Log(name + " south");
            south = true;
        }
        else if (IsTuple(lastCrossroads, crossroads, NorthWestCrossroads, SouthWestCrossroads))
        {
            Debug.Log(name + " west");
            west = true;
        }
        */
        CheckTombOpen();
    }

    private bool IsTuple(Crossroads c1, Crossroads c2, Crossroads c3, Crossroads c4) => (c1 == c3 && c2 == c4) || c1 == c4 || c2 == c3;


    private void CheckTombOpen()
    {
        if (!open && north && south && east && west)
        {
            open = true;
            OpenTomb();
        }
    }

    private void OpenTomb()
    {
        GetComponentInChildren<MeshRenderer>().material.color = Color.blue;
    }
}
