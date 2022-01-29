using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Text timeText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text maxScoreText;
    [SerializeField] private Text asteroidText;
    
    [SerializeField] private Text timeTextGamaOver;
    [SerializeField] private Text maxScoreTextGamaOver;
    [SerializeField] private Text asteroidTextGamaOver;
    [SerializeField] private Text scoreTextGamaOver;

    private int _maxScore;
    private int _countAsteroid;
    private float _timePassed;
    private int _scoreCount;
    
    void Start()
    {
        _countAsteroid = 0;
        _timePassed = 0;
        _maxScore = PlayerPrefs.GetInt("maxScore");
    }

    // Update the UI text
    private void Update()
    {
        _timePassed += Time.deltaTime;
        int minutes = (int) _timePassed / 60;
        int seconds = (int) _timePassed % 60;
        timeText.text = $"{minutes:00}:{seconds:00}";
        if (_scoreCount>_maxScore)
        {
            _maxScore = _scoreCount;
            PlayerPrefs.SetInt("maxScore",_maxScore);
            maxScoreText.color = Color.green;
        }
        maxScoreText.text = $"record score : {_maxScore}";
        scoreText.text = $"score : {_scoreCount}";
        asteroidText.text = $"asteroid : {_countAsteroid}";
        
        timeTextGamaOver.text = $"time : {timeText.text}";
        maxScoreTextGamaOver.text = maxScoreText.text;
        asteroidTextGamaOver.text = asteroidText.text;
        scoreTextGamaOver.text = $"score : {_scoreCount}";
    }

    /// <summary>
    /// Add points for the completed platform
    /// </summary>
    /// <param name="isBoost"></param>
    public void PlatformPassed(bool isBoost)
    {
        if (isBoost)
        {
            _scoreCount += 2;
        }
        else
        {
            _scoreCount += 1;
        }
    }

    /// <summary>
    /// Add points for passing an asteroid
    /// </summary>
    public void AsteroidPassed()
    {
        _scoreCount += 5;
        _countAsteroid += 1;
    }
}
