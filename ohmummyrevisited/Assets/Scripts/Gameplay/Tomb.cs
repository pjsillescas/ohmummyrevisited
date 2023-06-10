using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tomb : MonoBehaviour
{
    [SerializeField] private Crossroads NorthEastCrossroads;
    [SerializeField] private Crossroads NorthWestCrossroads;
    [SerializeField] private Crossroads SouthEastCrossroads;
    [SerializeField] private Crossroads SouthWestCrossroads;

    private bool north;
    private bool south;
    private bool east;
    private bool west;
    private bool open;
    private TombWanderer tombWanderer;

    // Start is called before the first frame update
    void Start()
    {
        tombWanderer = GameObject.FindGameObjectWithTag("Player").GetComponent<TombWanderer>();
        
        Crossroads.OnAnyActivatedCrossroads += ActivateCrossroads;
        north = false;
        south = false;
        east = false;
        west = false;
        open = false;
    }

    private void ActivateCrossroads(object sender, Crossroads crossroads)
	{
        var lastCrossroads = tombWanderer.GetLastCrossroads();

        if(NorthEastCrossroads == crossroads)
		{
            if(lastCrossroads == NorthWestCrossroads)
			{
                Debug.Log(name + " north");
                north = true;
			}
            else if(lastCrossroads == SouthEastCrossroads)
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

        //tombWanderer.SetLastCrossroads(crossroads);
        
        CheckTombOpen();
    }

    private void CheckTombOpen()
	{
        if(!open && north && south && east && west)
		{
            open = true;
            OpenTomb();
		}
	}

    private void OpenTomb()
	{
        Debug.Log("open");
        GetComponentInChildren<MeshRenderer>().material.color = Color.blue;
    }
}
