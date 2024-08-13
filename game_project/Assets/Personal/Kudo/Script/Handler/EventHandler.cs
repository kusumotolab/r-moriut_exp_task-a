using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventHandler : MonoBehaviour
{
    private int _eventCount;
    public event Action<int> EventOccurrence;
    public enum GameEvent
    {
        None,
        MONSTER_HOUSE
    }

    void Start()
    {
        _eventCount = 0;
    }

    public void MonsterHouse(SpawnManager spawnManager)
    {
        const int GENERATE_COUNT = 10;
        for(int i = 0; i < GENERATE_COUNT; i++)
        {
            int kind = UnityEngine.Random.Range(0, spawnManager.EnemyKindCount);
            spawnManager.SpawnEnemy(kind);
        }

        // For event publication.
        _eventCount += 1;
        EventOccurrence?.Invoke(_eventCount);
    }
}
