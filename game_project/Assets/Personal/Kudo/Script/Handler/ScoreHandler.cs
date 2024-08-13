using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    private int _currentScore;
    public int CurrentScore => _currentScore;

    void Start()
    {
        _currentScore = 0;
    }

    public void AddScore(int score)
    {
        _currentScore += score;
    }
}
