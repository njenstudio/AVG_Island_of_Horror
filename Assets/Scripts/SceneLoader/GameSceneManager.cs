using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Manager;
    public GameObject LoadingPrefab;
    public GameObject EnterScenePrefab;
    public GameObject ExitScenePrefab;
    //public Sprite[] LoadingIcons;
    //public Sprite[] LoadingBGs;
    int iconIndex = 0;
    public static Canvas MainCanvas;
    static bool isLoading = false;

    GameObject LoadingView;
    GameObject EnterAni;
    GameObject ExitAni;

    void Awake()
    {
        Inint();
    }

    void Inint()
    {
        if (Manager == null)
        {
            Manager = this;
            DontDestroyOnLoad(gameObject);
            List<GameObject> loadedSceneRoot = new List<GameObject>(SceneManager.GetActiveScene().GetRootGameObjects());
            MainCanvas = loadedSceneRoot.Find(f => f.tag == "Canvas").GetComponent<Canvas>();
        }
        else if (Manager != this)
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLoaded;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLoaded;
    }
    void OnLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Loaded: " + scene.name);

        //if (mode == LoadSceneMode.Additive)
        List<GameObject> loadedSceneRoot = new List<GameObject>(scene.GetRootGameObjects());
        MainCanvas = loadedSceneRoot.Find(f=>f.tag == "Canvas").GetComponent<Canvas>();

        if (scene.name == "Loading")
        {
            GameObject g = Instantiate(LoadingPrefab);
            LoadingView = g;
            RectTransform LoadingObj = g.GetComponent<RectTransform>();
            GameLoadingManager loading = g.GetComponent<GameLoadingManager>();

            LoadingObj.SetParent(MainCanvas.transform, false);
            LoadingObj.anchoredPosition = Vector3.zero;
        }
        else
        {
            GameObject g = Instantiate(ExitScenePrefab);
            ExitAni = g;
            GameLoadingManager exit = g.GetComponent<GameLoadingManager>();
            RectTransform ExitObj = g.GetComponent<RectTransform>();

            ExitObj.SetParent(MainCanvas.transform, false);
            ExitObj.anchoredPosition = Vector3.zero;

            if (mode == LoadSceneMode.Additive)
            {
                SceneManager.SetActiveScene(scene);
                SceneManager.UnloadSceneAsync("Loading");
                if (EnterAni != null)
                    Destroy(EnterAni);
                if (LoadingView != null)
                    Destroy(LoadingView);
            }
   
        }
    }

    void SetEnterLoading()
    {
        GameObject g = Instantiate(EnterScenePrefab);
        EnterAni = g;
        GameLoadingManager enterLoading = g.GetComponent<GameLoadingManager>();
        RectTransform Enter = g.GetComponent<RectTransform>();
        Enter.SetParent(MainCanvas.transform, false);
        Enter.anchoredPosition = Vector3.zero;
    }
    //Static voids
    public static void LoadAddGameScene(string scene)
    {
        if (isLoading)
            return;
        if (Time.timeScale < 1)
            Time.timeScale = 1;
        isLoading = true;
        if (Manager == null)
        {
            List<GameObject> loadedSceneRoot = new List<GameObject>(SceneManager.GetActiveScene().GetRootGameObjects());
            MainCanvas = loadedSceneRoot.Find(f => f.tag == "Canvas").GetComponent<Canvas>();
        }
        Manager.StartCoroutine(Manager.LoadAddScene(scene));
    }

    public static void LoadGameScene(string scene)
    {
        if (isLoading)
            return;
        if (Time.timeScale < 1)
            Time.timeScale = 1;
        isLoading = true;

        List<GameObject> loadedSceneRoot = new List<GameObject>(SceneManager.GetActiveScene().GetRootGameObjects());
        MainCanvas = loadedSceneRoot.Find(f => f.tag == "Canvas").GetComponent<Canvas>();
        //MainCanvas = GameObject.Find(f => f.tag == "Canvas").GetComponent<Canvas>();
        Manager.StartCoroutine(Manager.LoadNewScene(scene));
    }
    public static void DelayUnloadScene(string scene)
    {
        Manager.StartCoroutine(Manager._DelayUnloadScene(scene));
    }
    //IEnumerators
    IEnumerator LoadingScene()
    {
        yield return new WaitForSeconds(0.3f);
    }

    IEnumerator LoadNewScene(string scene)
    {
        Debug.Log("LoadStart:"+SceneManager.GetActiveScene().name);
        if(scene!="Title")
        {
            SetEnterLoading();
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("Loading", LoadSceneMode.Single);
            yield return LoadingScene();
        }

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene);
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;

        //When the load is still in progress, output the Text and progress bar
        while (!asyncOperation.isDone)
        {
            //m_Text.text = "Loading progress: " + (asyncOperation.progress * 100) + "%";
            //asyncOperation.allowSceneActivation = true;
            // Check if the load has finished
            
            if (asyncOperation.progress >= 0.9f)
            {

                //Wait to you press the space key to activate the Scene
                //Activate the Scene
                yield return new WaitForSeconds(1f);
                asyncOperation.allowSceneActivation = true;
                isLoading = false;
            }
            
            yield return null;
        }
    }

    IEnumerator LoadAddScene(string scene)
    {
        Debug.Log("LoadStart:" + SceneManager.GetActiveScene().name);

        List<GameObject> loadedSceneRoot = new List<GameObject>(SceneManager.GetActiveScene().GetRootGameObjects());
        MainCanvas = loadedSceneRoot.Find(f => f.tag == "Canvas").GetComponent<Canvas>();

        SetEnterLoading();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Loading", LoadSceneMode.Additive);
        yield return LoadingScene();
        
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;
        //Debug.Log("Pro :" + asyncOperation.progress);
        //When the load is still in progress, output the Text and progress bar
        while (!asyncOperation.isDone)
        {
            //m_Text.text = "Loading progress: " + (asyncOperation.progress * 100) + "%";
            //asyncOperation.allowSceneActivation = true;
            // Check if the load has finished

            if (asyncOperation.progress >= 0.9f)
            {

                //Wait to you press the space key to activate the Scene
                //Activate the Scene
                yield return new WaitForSeconds(1f);
                asyncOperation.allowSceneActivation = true;
                isLoading = false;
            }
            yield return null;
        }
    }

    IEnumerator _DelayUnloadScene(string scene)
    {
        yield return new WaitForSeconds(3);
        SceneManager.UnloadSceneAsync(scene, UnloadSceneOptions.None);
    }
}
