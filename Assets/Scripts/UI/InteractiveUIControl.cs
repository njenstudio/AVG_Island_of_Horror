using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveUIControl : MonoBehaviour
{
    public GameObject UIObject;
    public InteractiveUIView UIView;

    public Button Talk;
    public Button Feed;
    public Button TouchHead;
    public Button HoldHand;
    public Button Kiss;
    public Button Apologize;
    public Button TouchBreast;
    public Button Touchlowerbody;

    bool isShow = false;

    void Start()
    {
        SetUIValue();
    }

    public void ToggleUI()
    {
        isShow = !isShow;
        if(UIObject!=null)
        UIObject.SetActive(isShow);
    }
    [ContextMenu("SetUIValue")]
    public void SetUIValue()
    {
        if(UIView!=null)
        UIView.UpdateUIValue();
        SetButtons();
    }

    public void SetButtons()
    {
        #region Talk
        if (GameManager.Emotion < -20)//x
            Talk.gameObject.SetActive(true);
        else if (GameManager.Favorability >=100 && GameManager.Emotion >= -20)//b
            Talk.gameObject.SetActive(true);
        else if (GameManager.Favorability >= 0 && GameManager.Emotion >= -20)//a
            Talk.gameObject.SetActive(true);
        else
            Talk.gameObject.SetActive(false);
        #endregion
        #region Feed
        if (GameManager.Emotion < -20)//x
            Feed.gameObject.SetActive(true);
        else if (GameManager.Favorability >= 0 && GameManager.Emotion >= -20)//a
            Feed.gameObject.SetActive(true);
        else
            Feed.gameObject.SetActive(false);
        #endregion
        #region TouchHead
        if (GameManager.Favorability >=1000 && GameManager.Emotion >=-20)//c
            TouchHead.gameObject.SetActive(true);
        else if (GameManager.Favorability >= 500 && GameManager.Emotion >= 0)//b
            TouchHead.gameObject.SetActive(true);
        else if (GameManager.Favorability >= 100)//a
            TouchHead.gameObject.SetActive(true);
        else
            TouchHead.gameObject.SetActive(false);
        #endregion
        #region HoldHand
        if (GameManager.Favorability >= 1000 && GameManager.Emotion >= 0)//c
            HoldHand.gameObject.SetActive(true);
        else if (GameManager.Favorability >= 500 && GameManager.Emotion >= 20)//b
            HoldHand.gameObject.SetActive(true);
        else if (GameManager.Favorability >= 300 && GameManager.Emotion >= 50)//a
            HoldHand.gameObject.SetActive(true);
        else
            HoldHand.gameObject.SetActive(false);
        #endregion
        #region Kiss
        if (GameManager.Favorability >= 1000 && GameManager.Emotion >= 0)//c
            Kiss.gameObject.SetActive(true);
        else if (GameManager.Favorability >= 500 && GameManager.Emotion >= 20)//b
            Kiss.gameObject.SetActive(true);
        else if (GameManager.Favorability >= 300 && GameManager.Emotion >= 50)//a
            Kiss.gameObject.SetActive(true);
        else
            Kiss.gameObject.SetActive(false);
        #endregion
        #region Apologize
        if (GameManager.Favorability >= 0 && GameManager.Emotion < -20)//x
            Apologize.gameObject.SetActive(true);
        else
            Feed.gameObject.SetActive(false);
        #endregion
        #region TouchBreast
        if (TouchBreast != null)
        {
            if (GameManager.Emotion < -20)//x
                TouchBreast.gameObject.SetActive(true);
            else if (GameManager.Favorability >= 100 && GameManager.Emotion >= 50)//c
                TouchBreast.gameObject.SetActive(true);
            else if (GameManager.Favorability >= 100 && GameManager.Emotion >= -20)//b
                TouchBreast.gameObject.SetActive(true);
            else if (GameManager.Favorability >= 0 && GameManager.Emotion >= -20)//a
                TouchBreast.gameObject.SetActive(true);
            else
                TouchBreast.gameObject.SetActive(false);
        }
        #endregion
        #region Touchlowerbody
        if (Touchlowerbody != null)
        {
            if (GameManager.Emotion < -20)//x
                Touchlowerbody.gameObject.SetActive(true);
            else if (GameManager.Favorability >= 100 && GameManager.Emotion >= -20)//b
                TouchBreast.gameObject.SetActive(true);
            else if (GameManager.Favorability >= 0 && GameManager.Emotion >= -20)//a
                TouchBreast.gameObject.SetActive(true);
            else
                TouchBreast.gameObject.SetActive(false);
        }
        #endregion
    }

    public void TakeAction(string actionName)
    {
        switch(actionName)
        {
            case "Talk":
                InteractiveManager.TalkResult();
                break;
        }
    }
}
