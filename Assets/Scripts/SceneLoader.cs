using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : Singleton<SceneLoader>
{
    private AsyncOperation load = null;

    public void StartLoad(string name)
    {
        if(this.load == null)
        {
            this.load = SceneManager.LoadSceneAsync(name);
            this.load.allowSceneActivation = false;
        }
        else
        {
            Debug.LogWarning("Attempted to load scene while loading another scene");
        }
    }

    public void SwitchScenes()
    {
        this.load.allowSceneActivation = true;
        this.load = null;
    }
}
