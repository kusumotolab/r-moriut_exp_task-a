using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultToTitleTriggerObserver : SceneTransitionTriggerObserverBase
{
    protected override bool ShouldTrigger()
    {
        // �X�y�[�X�L�[�ŏI��
        // UI �̉��o�҂����ق����������͗v����
        return Input.GetKeyDown(KeyCode.Space);
    }
}
