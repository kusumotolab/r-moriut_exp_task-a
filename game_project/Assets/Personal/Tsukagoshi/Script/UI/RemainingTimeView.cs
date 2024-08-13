using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RemainingTimeView : MonoBehaviour
{
    [SerializeField]
    private TimeManager timeManager;

    /// <summary>�c�莞�Ԃ́u���v��\������ TextMesh</summary>
    [SerializeField]
    private TextMeshProUGUI remainingMinTextMesh;

    /// <summary>�c�莞�Ԃ́u�b�v��\������ TextMesh</summary>
    [SerializeField]
    private TextMeshProUGUI remainingSecTextMesh;

    // Start is called before the first frame update
    void Awake()
    {
        if (timeManager == null || remainingMinTextMesh == null || remainingSecTextMesh == null)
        {
            Debug.LogError("Required components is not assigned!");
            this.enabled = false;
        } 
    }

    // Update is called once per frame
    void Update()
    {
        remainingMinTextMesh.text = timeManager.Minute.ToString();
        remainingSecTextMesh.text = timeManager.Seconds.ToString("D2");
    }
}
