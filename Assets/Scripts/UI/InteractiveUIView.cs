using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveUIView : MonoBehaviour
{
    public MainUIView.GridNumberItems Emotion;
    public MainUIView.GridNumberItems ActionPoint;
    public UnityEngine.UI.Text t_Favorability;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateUIValue()
    {
        Emotion.Set(GameManager.Emotion / 10);
        ActionPoint.Set(GameManager.ActionPoint);
        t_Favorability.text = "" + GameManager.Favorability;
    }
}
