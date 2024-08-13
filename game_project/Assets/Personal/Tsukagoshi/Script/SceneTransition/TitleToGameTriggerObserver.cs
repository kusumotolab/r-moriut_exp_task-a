using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleToGameTriggerObserver : SceneTransitionTriggerObserverBase
{
    protected override bool ShouldTrigger()
    {
        // �X�y�[�X�L�[�ŊJ�n
        return Input.GetKeyDown(KeyCode.Space);
    }
}
