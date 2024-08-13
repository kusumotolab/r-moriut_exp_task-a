using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeUIControllerBase : MonoBehaviour
{
    [SerializeField]
    private RectTransform gaugeBody;
    [SerializeField]
    private RectTransform gaugeBG;

    private float initialWidth;
    private float initialHeight;

    [SerializeField]
    private Color gaugeColor;

    // Start is called before the first frame update
    void Start()
    {
        initialWidth = gaugeBody.rect.width;
        initialHeight = gaugeBody.rect.height;

        var image = gaugeBody.GetComponent<Image>();
        if (image != null)
        {
            image.color = gaugeColor;
        }
    }

    /// <summary>指定した割合にゲージ UI の長さを指定する</summary>
    /// <param name="rate"></param>
    public void setGaugeWidth(float rate)
    {
        gaugeBody.sizeDelta = new Vector2(initialWidth * rate, initialHeight);
    }

    /// <summary>
    /// ゲージの表示非表示を変更する
    /// </summary>
    /// <param name="visible"></param>
    public void setGaugeVisible(bool visible)
    {
        var gaugeBodyImage = gaugeBody?.GetComponent<Image>();
        if (gaugeBodyImage != null)
        {
            gaugeBodyImage.enabled = visible;
        }

        var gaugeBGImage = gaugeBG?.GetComponent<Image>();
        if (gaugeBGImage != null)
        {
            gaugeBGImage.enabled = visible;
        }
    }
}
