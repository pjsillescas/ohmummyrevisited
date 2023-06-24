using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    [SerializeField] private List<Image> LiveImages;

    private int livesRemaining;

    // Start is called before the first frame update
    void Start()
    {
        livesRemaining = LiveImages.Count;

        LiveImages.ForEach(image => image.enabled = true);

        GameObject.FindGameObjectWithTag("Player").GetComponent<LifeComponent>().OnLifeOver += PlayerOnLifeOver;
    }

    private void PlayerOnLifeOver(object sender, EventArgs args)
    {
        if (livesRemaining > 0)
        {
            livesRemaining--;
            StartCoroutine(HideCurrentLife());

            if (livesRemaining == 0)
            {
                Debug.Log("game over");
            }
        }
        else
		{
            Debug.Log("game over again!!");
		}
    }

    private IEnumerator HideCurrentLife()
	{
        LiveImages[livesRemaining].enabled = false;
        yield return null;
	}
}
