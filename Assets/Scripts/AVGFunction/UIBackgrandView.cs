using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIBackgrandView : MonoBehaviour
{
    [SerializeField]
    public List<BackgrandItem> backgrandItems = new List<BackgrandItem>();
    Image image;

    void Start()
    {
        image = GetComponent<Image>();
    }

    public void SetBackgrandItem(int index)
    {
        if(backgrandItems.Count>=index)
        {
            image.sprite = backgrandItems[index].sprite;
        }
        else
        {
            Debug.LogError("SetBackgrandItem Index over setting list count");
        }
    }

}
[System.Serializable]
public class BackgrandItem
{
    public string name;
    public Sprite sprite;
}
