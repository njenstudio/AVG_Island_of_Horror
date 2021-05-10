using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLoadingManager : MonoBehaviour
{
    public Text levelText;
    public Image icon;
    public Image BG;

    public void Inint(string text,Sprite spriteIcon, Sprite spriteBG)
    {
        if(levelText!=null)
            levelText.text = text;
        if (icon != null)
            icon.sprite = spriteIcon;
        if (BG != null)
            BG.sprite = spriteBG;
    }
}
