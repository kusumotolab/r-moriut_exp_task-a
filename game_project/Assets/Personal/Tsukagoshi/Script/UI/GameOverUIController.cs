using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>ゲームオーバー表示をコントロールする</summary>
public class GameOverUIController : MonoBehaviour
{
    [SerializeField]
    private GameObject Text;

    [SerializeField]
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        Text?.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if ((bool)gameManager?.IsGameOver)
        {
            Text?.SetActive(true);
        }
    }
}
