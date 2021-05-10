using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIZoom : MonoBehaviour
{
    public RectTransform targetPanel;
    public RectTransform targetUI;
    public Image imageOffset;
    public Vector3 zoomScale = new Vector3(2,2,2);
    public float zoomDruation = 0.5f;

    [ContextMenu("TestZoomInToCenter")]
    public void ZoomInToCenter()
    {
        StartCoroutine(_ZoomInToCenter());
    }
    [ContextMenu("TestZoomOut")]
    public void ZoomOut()
    {
        StartCoroutine(_ZoomOut());
    }

    Vector2 BeforZoomPos;
    Vector3 BeforZoomScale;
    IEnumerator _ZoomInToCenter()
    {
        BeforZoomPos = targetPanel.anchoredPosition;
        BeforZoomScale = targetPanel.localScale;
        Vector2 zoomTo = new Vector2(targetUI.anchoredPosition.x * (zoomScale.x * -1), targetUI.anchoredPosition.y * (zoomScale.y * -1));
        float d = 0f;

        while(d < zoomDruation)
        {
            targetPanel.anchoredPosition = Vector3.Lerp(targetPanel.anchoredPosition, zoomTo, d/zoomDruation);
            targetPanel.localScale = Vector3.Lerp(targetPanel.localScale, zoomScale, d/zoomDruation);
            d += Time.deltaTime;
            yield return null;
        }
        targetPanel.anchoredPosition = zoomTo;
        targetPanel.localScale = zoomScale;

    }

    IEnumerator _ZoomOut()
    {
        Vector2 zoomTo = BeforZoomPos;
        float d = 0f;

        while (d < zoomDruation)
        {
            targetPanel.anchoredPosition = Vector3.Lerp(targetPanel.anchoredPosition, zoomTo, d / zoomDruation);
            targetPanel.localScale = Vector3.Lerp(targetPanel.localScale, BeforZoomScale, d / zoomDruation);
            d += Time.deltaTime;
            yield return null;
        }
        targetPanel.anchoredPosition = zoomTo;
        targetPanel.localScale = BeforZoomScale;
    }
}
