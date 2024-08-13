using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultToTitleTriggerObserver : SceneTransitionTriggerObserverBase
{
    protected override bool ShouldTrigger()
    {
        // スペースキーで終了
        // UI の演出待ったほうがいいかは要検討
        return Input.GetKeyDown(KeyCode.Space);
    }
}
