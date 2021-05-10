using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIView : MonoBehaviour
{
    [System.Serializable]
    public class GridNumberItems
    {
        public Image[] imageViews;
        public Image[] imageBGViews;
        public int number;

        public void Set(int value)
        {
            number = value;
            for(int i = 0; i<imageViews.Length;i++ )
            {
                if (number > i)
                {
                    imageViews[i].gameObject.SetActive(true);
                    if (imageBGViews.Length > 0)
                        imageBGViews[i].gameObject.SetActive(false);
                }
                else if(imageBGViews.Length>0)
                    imageBGViews[i].gameObject.SetActive(true);
                else
                    imageViews[i].gameObject.SetActive(false);
            }
        }
    }

    public Text Favorability;

    public GridNumberItems Emotion;
    public GridNumberItems ActionPoint;
    //public Text Hungry;
    //turn main procces
    public Text Violet;
    public Text VioletProgress;
    public Text Target_Violet;
    public Image VioletBar;
    public Text Deadline;

    //public Text Turn;
    //public Text Clean;
    public Text Money;
    //public Text Contribution;
    public Text Days;

    public void UpdateUIValue()
    {
        Emotion.Set(GameManager.Emotion/10);
        ActionPoint.Set(GameManager.ActionPoint);
        Favorability.text = ""+GameManager.Favorability;
        Violet.text = "" + GameManager.Violet;
        Target_Violet.text = "" + GameManager.Target_Violet;
        Deadline.text = "" + GameManager.Deadline;
        Money.text = "" + GameManager.Money;
        Days.text = "" + GameManager.Days;
        float v = GameManager.Violet, tv = GameManager.Target_Violet;
        float progress = (v / tv);
        VioletBar.fillAmount = progress;
        VioletProgress.text = Mathf.Round(progress * 100) + "%";
    }

}
