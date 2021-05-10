using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Fungus;
using UnityEngine.SceneManagement;

namespace CarterGames.Assets.SaveManager
{
    public class SaveControl : MonoBehaviour
    {
        public GameObject pendingWindow;
        public Text confirmText;
        public Button confirmButton;
        public UnityEvent OnSave;
        public UnityEvent OnLoad;
        public Camera screenShotCam;
        public GameObject view;
        SaveView currentView;

        public static SaveControl SaveInstance;
        public static SaveControl Loadinstance;
        private void Start()
        {
            if(gameObject.name.Contains("Save"))
            {
                if (SaveInstance != null)
                {
                    Destroy(gameObject);
                    return;
                }
                SaveInstance = this;
            }
            else
            {
                if (Loadinstance != null)
                {
                    Destroy(gameObject);
                    return;
                }
                Loadinstance = this;
            }


            var saveManager = FungusManager.Instance.SaveManager;

            // Make a note of the current scene. This will be used when restarting the game.
            if (string.IsNullOrEmpty(saveManager.StartScene))
            {
                saveManager.StartScene = SceneManager.GetActiveScene().name;
            }
        }

        public void Save(int saveGrid)
        {
            StartCoroutine(_Save(saveGrid));
        }

        IEnumerator _Save(int saveGrid)
        {
            confirmButton.onClick.RemoveAllListeners();
            confirmText.text = "Is sure overwrite save data?";
            pendingWindow.SetActive(true);
            SaveData saveData = new SaveData();
            // Heroine datas
            saveData.HeroineName = GameManager.HeroineName;
            saveData.Favorability = GameManager.Favorability;
            saveData.Emotion = GameManager.Emotion;
            saveData.Hungry = GameManager.Hungry;
            //turn main procces
            saveData.Violet = GameManager.Violet;
            saveData.Target_Violet = GameManager.Target_Violet;
            saveData.Deadline = GameManager.Deadline;
            saveData.ActionPoint = GameManager.ActionPoint;
            saveData.Turn = GameManager.Turn;
            saveData.Clean = GameManager.Clean;
            saveData.Money = GameManager.Money;
            saveData.Contribution = GameManager.Contribution;
            saveData.Days = GameManager.Days;
            saveData.eventDatas = GameManager.eventDatas;
            saveData.saveDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            saveData.saveScene = SceneManager.GetActiveScene().name;
            if (Flowchart.CachedFlowcharts.Count>0)
            {
                saveData.flowchartName = Flowchart.CachedFlowcharts[0].GetName();
                saveData.blockName = Flowchart.CachedFlowcharts[0].SelectedBlock.name;
            }
            string ScreenPath = Application.dataPath + "/saveScreen" + saveGrid + ".png";
            yield return TakeScreenShot(ScreenPath);
            saveData.screenPath = ScreenPath;
            confirmButton.onClick.AddListener(() => {
                SaveManager.SaveGame(saveData, saveGrid);
                pendingWindow.SetActive(false);
                currentView.UpdataView();
                OnSave.Invoke();
            });


        }

        public void Load(int saveGrid)
        {
            confirmButton.onClick.RemoveAllListeners();
            confirmText.text = "Is sure load save data?";
            pendingWindow.SetActive(true);
            confirmButton.onClick.AddListener(() => {
                SaveData saveData = new SaveData();
                saveData = SaveManager.LoadGame(saveGrid);
                // Heroine datas
                GameManager.HeroineName = saveData.HeroineName;
                GameManager.Favorability = saveData.Favorability;
                GameManager.Emotion = saveData.Emotion;
                GameManager.Hungry = saveData.Hungry ;
                //turn main procces
                GameManager.Violet = saveData.Violet;
                GameManager.Target_Violet = saveData.Target_Violet;
                GameManager.Deadline = saveData.Deadline;
                GameManager.ActionPoint = saveData.ActionPoint;
                GameManager.Turn = saveData.Turn;
                GameManager.Clean = saveData.Clean;
                GameManager.Money = saveData.Money;
                GameManager.Contribution = saveData.Contribution;
                GameManager.Days = saveData.Days;
                GameManager.eventDatas = saveData.eventDatas;
                if (MainUIControl.instance != null)
                    MainUIControl.instance.SetUIValue();
                pendingWindow.SetActive(false);

                if (saveData.flowchartName.Length>0)
                {
                    Destroy(Flowchart.CachedFlowcharts[0].gameObject);
                }
                UnityAction<Scene, LoadSceneMode> onSceneLoadedAction = null;
                onSceneLoadedAction = (scene, mode) =>
                {
                    if (mode == LoadSceneMode.Additive ||
                        scene.name != saveData.saveScene)
                    {
                        return;
                    }
                    SceneManager.sceneLoaded -= onSceneLoadedAction;
                    if (saveData.flowchartName.Length > 0)
                    {
                        GameObject dialog = SayDialogManager.InstantiateSayDialog(saveData.flowchartName,GameManager.instance.transform);
                        Flowchart f = dialog.GetComponent<Flowchart>();
                        f.ExecuteIfHasBlock(saveData.blockName);
                    }
                    OnLoad.Invoke();
                };
                SceneManager.sceneLoaded += onSceneLoadedAction;
                GameSceneManager.LoadGameScene(saveData.saveScene);

            });
        }

        public void SetView(GameObject viewObj)
        {
            currentView = viewObj.GetComponent<SaveView>();
        }

        public IEnumerator TakeScreenShot(string path)
        {
            yield return new WaitForEndOfFrame();

            Camera camOV = screenShotCam;
            RenderTexture currentRT = RenderTexture.active;
            RenderTexture.active = camOV.targetTexture;
            camOV.Render();
            Texture2D imageOverview = new Texture2D(camOV.targetTexture.width, camOV.targetTexture.height, TextureFormat.RGB24, false);
            imageOverview.ReadPixels(new Rect(0, 0, camOV.targetTexture.width, camOV.targetTexture.height), 0, 0);
            imageOverview.Apply();
            RenderTexture.active = currentRT;

            // Encode texture into PNG
            byte[] bytes = imageOverview.EncodeToPNG();

            // save in memory
            System.IO.File.WriteAllBytes(path, bytes);
        }

    }
}

