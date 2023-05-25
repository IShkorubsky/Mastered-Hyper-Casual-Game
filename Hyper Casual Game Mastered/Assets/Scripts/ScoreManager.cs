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
    
    private int _currentScore;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        _currentScore = 0;
        bestScoreText.text = PlayerPrefs.GetInt("Best").ToString();
    }

    public void AddScore()
    {
        _currentScore++;
        currentScoreText.text = _currentScore.ToString();
        if (_currentScore > PlayerPrefs.GetInt("Best",0))
        {
            bestScoreText.text = _currentScore.ToString();
            PlayerPrefs.SetInt("Best",_currentScore);
        }
    }
}
