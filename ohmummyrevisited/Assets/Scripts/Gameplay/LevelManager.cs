using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance = null;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
