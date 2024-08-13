using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleToGameTriggerObserver : SceneTransitionTriggerObserverBase
{
    protected override bool ShouldTrigger()
    {
        // スペースキーで開始
        return Input.GetKeyDown(KeyCode.Space);
    }
}
