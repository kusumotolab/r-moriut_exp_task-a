using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Off-screen threshold.
    private float outUndeler = -0.2f;
    private float outUpper = 1.2f;
    [SerializeField]
    private GameObject[] _enemy;
    public int EnemyKindCount => _enemy.Length;

    public void SpawnEnemy(int kind)
    {
        // Fixed for 0_X, 1_Y 
        int rnd = Random.Range(0, 2);
        Vector2 setThreshold = CalcInsPos(rnd);
        
        // Camera.main.ViewportToWorldPoint --- Lower left -0,0 Upper right 1,1.
        Vector3 insPosition = Camera.main.ViewportToWorldPoint(new Vector3(setThreshold.x, setThreshold.y, Camera.main.nearClipPlane));
        insPosition.z = 0;
        Instantiate (_enemy[kind], insPosition, Quaternion.identity);
    }

    private Vector2 CalcInsPos(int fixedDirection)
    {
        Vector2 insPos = Vector2.zero;
        if(fixedDirection == 0)
        {
            float rndDirection = Random.Range(0, 2);
            if(rndDirection == 0) insPos.x = outUndeler;
            else insPos.x = outUpper;
            float rndPos = Random.Range(0.0f, 1.0f);
            insPos.y = rndPos;
        }
        else
        {
            float rndDirection = Random.Range(0, 2);
            if(rndDirection == 0) insPos.y = outUndeler;
            else insPos.y = outUpper;
            float rndPos = Random.Range(0.0f, 1.0f);
            insPos.x = rndPos;
        }

        return insPos;
    }
}
