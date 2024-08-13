using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameToResultTriggerObserver : SceneTransitionTriggerObserverBase
{
    [SerializeField]
    private GameManager gameManager;
    protected override bool ShouldTrigger()
    {
        // �Q�[���N���A�̏���������
        return gameManager.IsNextScene;
    }
}
