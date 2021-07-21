using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SliderGridView : MonoBehaviour
{
    public Image[] imageViews;
    public Image[] imageBGViews;
    public int number;

    public void Set(int value)
    {
        number = value;
        for (int i = 0; i < imageViews.Length; i++)
        {
            if (number > i)
            {
                imageViews[i].gameObject.SetActive(true);
            }
            else
                imageViews[i].gameObject.SetActive(false);
        }
    }
}
