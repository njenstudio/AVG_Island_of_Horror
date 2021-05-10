using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;
    public List<Town> towns = new List<Town>();
    public List<BattleView> views = new List<BattleView>();
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void RecoverAllTownStable()
    {
        for (int i = 0; i < towns.Count; i++)
            RecoverStable(towns[i]);
    }

    public void RecoverStable(Town town)
    {
        float basis = town.isBreak ? 0.15f : 0.03f;
        float r = (town.MaxStable * basis) + (town.Stable * 0.1f);
        town.Stable += r;
        if (town.Stable > town.MaxStable)
            town.Stable = town.MaxStable;
    }

    public void AttackTown(string townName)
    {
        Town town = towns.Find(f => f.Name == townName);

    }
}
[System.Serializable]
public class Town
{
    public string Name;
    public float MaxStable = 1000;
    public float Stable = 1000;
    public int Known = 0;
    public bool isBoost = false;
    public bool isDisrupt = false;
    public bool isEvent = false;
    public bool isBreak = false;
}
