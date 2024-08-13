using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUIController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI selfScoreTextMesh;

    [SerializeField]
    // 0:bottom(3��) ��z��
    private TextMeshProUGUI[] prevScoreTextMeshes;

    // Start is called before the first frame update
    void Start()
    {
        var scoreManager = ScoreManager.InstanceScoreManager;
        var highScores = scoreManager.Top3Score;

        // �����̏���
        var currentRank = scoreManager.CurrentRank;

        // ���X�R�A
        var selfScore = scoreManager.CurrentScore;
        selfScoreTextMesh.text = selfScore.ToString();

        // 3��
        setHighScoreText(highScores, 0, selfScore);

        // 2��
        setHighScoreText(highScores, 1, selfScore);

        // 1��
        setHighScoreText(highScores, 2, selfScore);
    }

    // 1�ʁ`�R�ʂ̃n�C�X�R�A�\��������
    private void setHighScoreText(int[] highScoreRanks, int rank, int selfScore)
    {
        var highScore = highScoreRanks[rank];
        var highScoreTextMesh = prevScoreTextMeshes[rank];
        // �L�^�Ȃ��̏ꍇ�͕\�����Ȃ�
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
