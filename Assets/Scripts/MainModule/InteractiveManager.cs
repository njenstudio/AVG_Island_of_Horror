using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveManager : MonoBehaviour
{
    public static InteractiveManager instance;
    public InteractiveUIControl MainUIControl;
    public InteractiveUIControl UIControl;


    void Start()
    {
        instance = this;
    }

    public void StartInteractive()
    {
        UIControl.ToggleUI();
        UIControl.SetUIValue();
    }

    int lastFavorabilityChange = 0;
    int plusFavorabilityCount = 0;
    void ModifyStatus(int favorability,int Emotion)
    {
        int finalFvo = favorability;
        int finalFeel = Emotion;

        if (favorability > 0)
        {
            plusFavorabilityCount++;
        }
        else
        {
            plusFavorabilityCount = 0;
            finalFvo--;
            finalFeel -= 2;
        }

        if (GameManager.Emotion >= 50)//shy
        {
            finalFeel += 5;
            //TODO:ChangeCharacterMood
        }

        if (plusFavorabilityCount>1)//happy
        {
            finalFvo++;
            finalFeel += 2;
            //TODO:ChangeCharacterMood
        }
        //TODO:Angry

        GameManager.Favorability += finalFvo;
        GameManager.Emotion += finalFeel;
        GameManager.ActionPoint--;

        GameManager.Hungry -= 10;
        if(GameManager.Hungry<-20)
            GameManager.Hungry = -20;

        GameManager.instance.mainUI.SetUIValue();
        InteractiveManager.instance.UIControl.SetUIValue();
        InteractiveManager.instance.MainUIControl.SetUIValue();
       lastFavorabilityChange = favorability;
    }

    public static void TalkResult()
    {
        int r = Random.Range(0, 100);
        int getIndex = 0;
        List<int> chances = new List<int>();
        List<int> FavorabilityChanges = new List<int>();
        List<int> EmotionChanges = new List<int>();
        if (GameManager.Emotion < -20)//x
        {
            int[] chances_x = { 20, 80 };
            int[] FavorabilityChanges_x = { -2,0};
            int[] EmotionChanges_x = {-3,0 };
            chances.AddRange(chances_x);
            FavorabilityChanges.AddRange(FavorabilityChanges_x);
            EmotionChanges.AddRange(EmotionChanges_x);
        }
        else if (GameManager.Favorability >= 100 && GameManager.Emotion >= -20)//b
        {
            int[] chances_b = { 10, 10, 50, 30 };
            int[] FavorabilityChanges_b = { -2, 1, 5, 8 };
            int[] EmotionChanges_b = { 0, 2, 8, 12 };
            chances.AddRange(chances_b);
            FavorabilityChanges.AddRange(FavorabilityChanges_b);
            EmotionChanges.AddRange(EmotionChanges_b);
        }
        else if (GameManager.Favorability >= 0 && GameManager.Emotion >= -20)//a
        {
            int[] chances_a = { 10, 10, 50, 30 };
            int[] FavorabilityChanges_a = { -2, 1, 3, 5 };
            int[] EmotionChanges_a = { -1, 0, 5, 10 };
            chances.AddRange(chances_a);
            FavorabilityChanges.AddRange(FavorabilityChanges_a);
            EmotionChanges.AddRange(EmotionChanges_a);
        }

        int chance = 0;
        for (int i = 0; i < chances.Count; i++)
        {
            chance += chances[i];
            if (r <= chance)
            {
                getIndex = i;
                break;
            }
            else
            {
                getIndex = chances.Count - 1;
            }
        }
        instance.ModifyStatus(FavorabilityChanges[getIndex], EmotionChanges[getIndex]);
        //TODO:Show AVG  
        if(GameManager.Hungry<=20)
        {
            //TODO:AddSayCommand to last say.
        }

    }

    public static void FeedResult()
    {
        GameManager.Hungry += 40;
        if (GameManager.Hungry > 100)
            GameManager.Hungry = 100;
        //TODO:Show AVG  
    }

}
