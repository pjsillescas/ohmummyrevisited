using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TombWanderer : MonoBehaviour
{
    private Crossroads lastCrossroads;

    // Start is called before the first frame update
    void Start()
    {
        lastCrossroads = null;
    }

    public void SetLastCrossroads(Crossroads crossroads)
	{
        if(lastCrossroads != null)
		{
            lastCrossroads.Deactivate();
		}

        lastCrossroads = crossroads;
	}

    public Crossroads GetLastCrossroads() => lastCrossroads;
}
