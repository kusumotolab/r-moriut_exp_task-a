using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager InstanceScoreManager;
    // 0_bottom
    private int[] _top3Score =  new int[]{ -1, -1, -1};
    public int[] Top3Score => _top3Score;

    // currenct_rank.
    private int _currentScore;
    public int CurrentScore => _currentScore;

    // currenct_rank.
    private int _currentRank;
    public int CurrentRank => _currentRank;
    void Awake()
    {
        if(InstanceScoreManager != null) 
        {
            Destroy(this);
            return;
        }
        InstanceScoreManager = this;
        DontDestroyOnLoad(this);
    }

    public void SortRanking(int newScore)
    {
        _currentScore = newScore;
        _currentRank= _top3Score.Length + 1;
        if(newScore <= _top3Score[0]) return;
        _top3Score[0] = newScore;
        // Sort Rank.
        Array.Sort(_top3Score);
        // Selection of Rank.
        for(int i = 0; i < _top3Score.Length; i++)
        {
            if(_top3Score[i] != newScore) continue;
            _currentRank = 3 - i;
            break;
        }
    }

}
