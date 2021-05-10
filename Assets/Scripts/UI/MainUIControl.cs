using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIControl : MonoBehaviour
{
    public static MainUIControl instance;
    public GameObject UIObject;
    public MainUIView UIView;
    bool isShow = false;
    Animator animator;//if has fadein animator

    void Start()
    {
        instance = this;
    }

    public void ToggleUI()
    {
        isShow = !isShow;
        UIObject.SetActive(isShow);
    }

    public void SetUIValue()
    {
        UIView.UpdateUIValue();
    }
}
