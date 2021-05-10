using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace CarterGames.Assets.SaveManager
{
    public class SaveView : MonoBehaviour
    {
        public Text SaveNumberText;
        public Text TurnNumberText;
        public Text DateText;
        public Image screenShot;
        public int saveGrid;
        public bool isAutoSave = false;
        // Start is called before the first frame update
        void Start()
        {
            UpdataView();
        }

        public void UpdataView()
        {
            SaveData data = new SaveData();
            data = SaveManager.LoadGame(saveGrid);
            if (!isAutoSave)
                SaveNumberText.text = "No.0" + saveGrid;
            else
                SaveNumberText.text = "AutoSave";
            if (data!=null)
            {
                TurnNumberText.text = "Turn:" + data.Turn;
                DateText.text = "" + data.saveDate;

                Texture2D t = SaveManager.LoadPNG(data.screenPath);
                if(t!=null)
                {
                    Sprite s = Sprite.Create(t, new Rect(0, 0, t.width, t.height), Vector2.zero);
                    screenShot.sprite = s;
                    screenShot.color = new Color(1, 1, 1);
                }
            }
        }
    }
}
