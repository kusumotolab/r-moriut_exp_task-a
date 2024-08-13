using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>シーンの状態を監視し、条件を満たすと遷移をトリガーするビヘイビアの基底</summary>
// 「タイトル→インゲーム」「インゲーム→リザルト」「リザルト→タイトル」への遷移へ派生することを想定
public abstract class SceneTransitionTriggerObserverBase : MonoBehaviour
{
    [SerializeField]
    protected string nextSceneName;

    // Update is called once per frame
    protected void Update()
    {
        // 遷移条件を満たしたら即時遷移する
        if (ShouldTrigger())
        {
            SceneLoader.Load(nextSceneName);
        }
    }

    /// <summary>シーン遷移をトリガーすべき時に True を返す</summary>
    protected abstract bool ShouldTrigger();
}
