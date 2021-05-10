using CarterGames.Assets.SaveManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    #region static ints
    // Heroine datas
    public static string HeroineName = "Heroine";
    public static int Favorability = 0;
    public static int Emotion = 0;
    public static int Hungry = 0;
    
    //turn main procces
    public static int Violet = 0;
    public static int Target_Violet = 1000;
    public static int Deadline = 14;
    public static int ActionPoint = 9;
    public static int Turn = 1;
    public static int Clean = 80;
    public static int Money = 500;
    public static int Contribution = 0;

    public static int Days = 1;
    #endregion

    public static List<EventData> eventDatas = new List<EventData>();

    public MainUIControl mainUI { get { return MainUIControl.instance; } }
    public InteractiveManager InteractiveManager { get { return InteractiveManager.instance; } }

    int DefaultDeadline = 14;
    bool isFristStart = true;
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
        if(isFristStart)
            OnGameStart();
        isFristStart = false;
    }
    //Frist time start game
    void OnGameStart()
    {
        GameObject Prologue = SayDialogManager.InstantiateSayDialog(SayDialogManager.PrefabName.PrologueFlowchart,transform);
        Prologue.GetComponent<InvokUnityEvent>().Event.AddListener(TurnStart);
        //Fungus.InvokeMethod invoke = Prologue.GetComponent<Fungus.InvokeMethod>();
        //invoke.TargetObject = this.gameObject;
    }

    [ContextMenu("TurnStart")]
    public void TurnStart()
    {
        if(Hungry >= 80)
        {
            Emotion += 5;
        }
        if (Hungry < 20)
        {
            Emotion -= 5;
        }
            
        mainUI.ToggleUI();
        mainUI.SetUIValue();
        SaveManager.AutoSave();
    }

    public void TurnEnd()
    {
        Turn ++;
        Deadline--;
        TurnResult();
    }

    void TurnResult()
    {
        if(Deadline<=0)
        {
            DeadLineResult();
        }
    }

    void DeadLineResult()
    {
        if(Violet>=Target_Violet)
        {
            Deadline = DefaultDeadline;
            TurnStart();
        }
        else
        {
            GameOver();
            return;
        }
    }

    void GameOver()
    {
        //TODO:ShowGameOver

    }
}
