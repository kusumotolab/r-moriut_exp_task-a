using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    
    [Header("ゲーム時間(秒)の設定")]
    [SerializeField]
    private float _setTime;
    public float SetTime => _setTime;
    // Remaining game time.
    private float _remainingTime;
    public float RemainingTime => _remainingTime;
    
    // Remaining game time.(minute).
    private int _minute;
    public int Minute => _minute;
    // Remaining game time(seconds).
    private int _seconds;
    public int Seconds => _seconds;

    // Start is called before the first frame update
    void Awake()
    {
        _remainingTime = _setTime;
    }

    public void CountDown(float elapsedTime)
    {
        _remainingTime -= elapsedTime;
        _minute = (int)_remainingTime / 60;
        _seconds = (int)_remainingTime % 60;
    }
}
