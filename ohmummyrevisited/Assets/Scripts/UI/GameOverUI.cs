using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance;

    [SerializeField] private Button RestartButton;
    [SerializeField] private Button MainMenuButton;
    [SerializeField] private GameObject Panel;

	private void Awake()
	{
		if(Instance != null)
		{
            Debug.LogError("There is another game over UI");
            return;
		}

        Instance = this;
        Hide();
	}

	// Start is called before the first frame update
	void Start()
    {
        RestartButton.onClick.AddListener(RestartClick);
        MainMenuButton.onClick.AddListener(MainMenuClick);

        //panel = GetComponentInChildren<Image>().gameObject;
    }

    private void RestartClick()
	{
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

    private void MainMenuClick()
	{
        ;
	}

    public void Show()
	{
        Panel.SetActive(true);
	}

    public void Hide()
    {
        Panel.SetActive(false);
    }

}
