using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IngameScoreUIController : MonoBehaviour
{
    private ScoreHandler scoreHandler;

    [SerializeField]
    private TextMeshProUGUI scoreTextMesh;

    // Start is called before the first frame update
    void Start()
    {
        scoreHandler = GameObject.FindWithTag("ScoreHandler")?.GetComponent<ScoreHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreTextMesh.text = scoreHandler.CurrentScore.ToString();
    }
}
