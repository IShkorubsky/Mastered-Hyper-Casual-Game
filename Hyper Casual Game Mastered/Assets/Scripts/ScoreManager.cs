using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;

    public static ScoreManager Instance;
    
    public int currentScore;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        currentScore = 0;
        bestScoreText.text = PlayerPrefs.GetInt("Best").ToString();
    }

    public void AddScore()
    {
        currentScore++;
        currentScoreText.text = currentScore.ToString();
        if (currentScore > PlayerPrefs.GetInt("Best",0))
        {
            bestScoreText.text = currentScore.ToString();
            PlayerPrefs.SetInt("Best",currentScore);
        }
    }
}
