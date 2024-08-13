using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    /// <summary>シーン名を指定してロードする</summary>
    public static void Load(string sceneName)
    {
        if ((bool)sceneName?.Equals(""))
        {
            Debug.LogError("Invalid scene name is Specified; Can't load a scene...");
            return;
        }

        Debug.Log($"Load Scene: {sceneName}");
        SceneManager.LoadScene(sceneName);
    }
}
