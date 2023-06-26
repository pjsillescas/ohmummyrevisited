using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;

    [SerializeField] private Image KeySprite;
    [SerializeField] private Image SarcophagusSprite;
    [SerializeField] private Image ScrollSprite;

	private void Awake()
	{
        if(Instance != null)
		{
            Debug.LogError("There is another inventory UI");
            return;
		}

        Instance = this;
        ResetInventory();
	}

    public void ResetInventory()
    {
        KeySprite.enabled = false;
        SarcophagusSprite.enabled = false;
        ScrollSprite.enabled = false;
    }

    public void SetKeyStatus(bool keyState)
	{
        KeySprite.enabled = keyState;
	}
    public void SetScrollStatus(bool scrollState)
    {
        ScrollSprite.enabled = scrollState;
    }
    public void SetSarcophagusStatus(bool sarcophagusState)
    {
        SarcophagusSprite.enabled = sarcophagusState;
    }
}
