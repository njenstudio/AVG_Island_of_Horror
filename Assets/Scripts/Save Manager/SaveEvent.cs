using CarterGames.Assets.SaveManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveEvent : MonoBehaviour
{
    SaveControl saveControl;

    public void OpenSaveWindow()
    {
        saveControl = SaveControl.SaveInstance;
        if (saveControl != null)
            saveControl.view.SetActive(true);
    }

    public void OpenLoadWindow()
    {
        saveControl = SaveControl.Loadinstance;
        if(saveControl!=null)
            saveControl.view.SetActive(true);
    }
}
