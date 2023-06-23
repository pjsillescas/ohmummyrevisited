using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TombAdvanced : MonoBehaviour
{
    public enum TombType { none, sarcophagus, chest, key, scroll, mummy }

    [SerializeField] private Transform NorthEast;
    [SerializeField] private Transform NorthWest;
    [SerializeField] private Transform SouthEast;
    [SerializeField] private Transform SouthWest;

    [SerializeField] private SpriteRenderer NorthSprite;
    [SerializeField] private SpriteRenderer EastSprite;
    [SerializeField] private SpriteRenderer SouthSprite;
    [SerializeField] private SpriteRenderer WestSprite;
    
    [SerializeField] private SpriteRenderer DeactivatedSprite;
    [SerializeField] private SpriteRenderer KeySprite;
    [SerializeField] private SpriteRenderer SarcophagusSprite;
    [SerializeField] private SpriteRenderer ScrollSprite;
    [SerializeField] private SpriteRenderer MummySprite;
    [SerializeField] private SpriteRenderer ChestSprite;
    [SerializeField] private SpriteRenderer EmptySprite;

    public event EventHandler<TombAdvanced> OnOpenTomb;

    private Crossroads NorthEastCrossroads;
    private Crossroads NorthWestCrossroads;
    private Crossroads SouthEastCrossroads;
    private Crossroads SouthWestCrossroads;

    private TombType type;

    private bool north;
    private bool south;
    private bool east;
    private bool west;
    private bool open;
    private TombWanderer tombWanderer;

	private void Awake()
	{
        ResetTomb(TombType.none);
        tombWanderer = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadCrossroads();

        Crossroads.OnAnyActivatedCrossroads += ActivateCrossroads;
    }

    public void ResetTomb(TombType type)
	{
        north = false;
        south = false;
        east = false;
        west = false;
        open = false;
        
        this.type = type;

        NorthSprite.enabled = false;
        EastSprite.enabled = false;
        SouthSprite.enabled = false;
        WestSprite.enabled = false;

        DeactivatedSprite.enabled = true;
        
        KeySprite.enabled = false;
        SarcophagusSprite.enabled = false;
        ScrollSprite.enabled = false;
        EmptySprite.enabled = false;
        ChestSprite.enabled = false;
        MummySprite.enabled = false;
    }

    public void SetTombType(TombType type)
	{
        //Debug.Log("name " + name + " type " + type);
        this.type = type;
	}

    public TombType GetTombType() => type;

	private void OnDestroy()
	{
        Crossroads.OnAnyActivatedCrossroads -= ActivateCrossroads;
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
                //Debug.Log(name + " north");
                north = true;
                NorthSprite.enabled = true;
            }
            else if (lastCrossroads == SouthEastCrossroads)
            {
                //Debug.Log(name + " east");
                east = true;
                EastSprite.enabled = true;
            }
        }
        else if (SouthEastCrossroads == crossroads)
        {
            if (lastCrossroads == NorthEastCrossroads)
            {
                //Debug.Log(name + " east");
                east = true;
                EastSprite.enabled = true;

            }
            else if (lastCrossroads == SouthWestCrossroads)
            {
                //Debug.Log(name + " south");
                south = true;
                SouthSprite.enabled = true;
            }
        }
        else if (NorthWestCrossroads == crossroads)
        {
            if (lastCrossroads == NorthEastCrossroads)
            {
                //Debug.Log(name + " north");
                north = true;
                NorthSprite.enabled = true;
            }
            else if (lastCrossroads == SouthWestCrossroads)
            {
                //Debug.Log(name + " west");
                west = true;
                WestSprite.enabled = true;
            }
        }
        else if (SouthWestCrossroads == crossroads)
        {
            if (lastCrossroads == NorthWestCrossroads)
            {
                //Debug.Log(name + " west");
                west = true;
                WestSprite.enabled = true;
            }
            else if (lastCrossroads == SouthEastCrossroads)
            {
                //Debug.Log(name + " south");
                south = true;
                SouthSprite.enabled = true;
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
        Color color;

        switch(type)
		{
            case TombType.mummy:
                color = Color.black;
                break;
            case TombType.scroll:
                color = Color.cyan;
                break;
            case TombType.chest:
                color = Color.yellow;
                break;
            case TombType.key:
                color = Color.white;
                break;
            case TombType.sarcophagus:
                color = Color.red;
                break;
            case TombType.none:
            default:
                color = Color.blue;
                break;
		}

        DeactivatedSprite.enabled = false;
        switch (type)
        {
            case TombType.mummy:
                MummySprite.enabled = true;
                break;
            case TombType.scroll:
                ScrollSprite.enabled = true;
                break;
            case TombType.chest:
                ChestSprite.enabled = true;
                break;
            case TombType.key:
                KeySprite.enabled = true;
                break;
            case TombType.sarcophagus:
                SarcophagusSprite.enabled = true;
                break;
            case TombType.none:
            default:
                EmptySprite.enabled = true;
                break;
        }
        
        //Debug.Log(name + " => " + type + " => " + color);
        GetComponentInChildren<MeshRenderer>().material.color = color;
        OnOpenTomb(this, this);
    }
}
