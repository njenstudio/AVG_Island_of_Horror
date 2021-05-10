using UnityEngine;

using UnityEngine.SceneManagement;
public class LoadScene : MonoBehaviour {

	public void LoadSceneByName(string name)
	{
        Debug.Log("LoadScene " + name);
        GameSceneManager.LoadGameScene(name);
	}

    public void LegacyLoadSceneByName(string name)
    {
        Debug.Log("LegacyLoadSceneByName " + name);
        SceneManager.LoadScene(name);
    }

    public void UnloadScene(string name)
    {
        SceneManager.UnloadSceneAsync(name,UnloadSceneOptions.None);
    }
}
