using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TextScore;

    private void SetScore(int score)
	{
        TextScore.text = string.Format("{0}",score);
	}

    // Start is called before the first frame update
    void Start()
    {
        LevelManager.Instance.OnScoreChanged += ScoreChanged;

        SetScore(0);
    }

    private void ScoreChanged(object sender, int score)
	{
        SetScore(score);
	}
}
