using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUIController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI selfScoreTextMesh;

    [SerializeField]
    // 0:bottom(3位) を想定
    private TextMeshProUGUI[] prevScoreTextMeshes;

    // Start is called before the first frame update
    void Start()
    {
        var scoreManager = ScoreManager.InstanceScoreManager;
        var highScores = scoreManager.Top3Score;

        // 自分の順位
        var currentRank = scoreManager.CurrentRank;

        // 自スコア
        var selfScore = scoreManager.CurrentScore;
        selfScoreTextMesh.text = selfScore.ToString();

        // 3位
        setHighScoreText(highScores, 0, selfScore);

        // 2位
        setHighScoreText(highScores, 1, selfScore);

        // 1位
        setHighScoreText(highScores, 2, selfScore);
    }

    // 1位～３位のハイスコア表示をする
    private void setHighScoreText(int[] highScoreRanks, int rank, int selfScore)
    {
        var highScore = highScoreRanks[rank];
        var highScoreTextMesh = prevScoreTextMeshes[rank];
        // 記録なしの場合は表示しない
        if (highScore < 0)
        {
            highScoreTextMesh.text = "----------------------";
        }
        else
        {
            highScoreTextMesh.text = highScore.ToString();
        }
        if (highScoreRanks[rank] == selfScore)
        {
            highScoreTextMesh.color = Color.yellow;
        }

    }
}
