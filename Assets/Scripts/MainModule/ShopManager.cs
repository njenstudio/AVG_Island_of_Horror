using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public List<Item> itemList = new List<Item>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void BuyItem()
    {

    }
}
[System.Serializable]
public class Item
{
    public Sprite icon = null;
    public string name = "";
    public string id = "";
    public int price = 0;
    public int count = 0;
    public bool isConsumables = false;
}
