using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// В данной реализации это скорее не полноценный загрузчик, а эдакая заглушка.
///  Просто на старте загружает имеющиеся сцены.
/// </summary>
public class SceneLoader : MonoBehaviour
{
    private int _sceneLoadedIndex = 1;

    /// <summary>
    /// Возвращает root-gameobject сцены.
    /// </summary>    
    public event Action<GameObject> OnSceneLoaded;

    public event Action OnAllScenesLoaded;

    public void LoadGameScenes()
    {
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            SceneManager.LoadSceneAsync(i, LoadSceneMode.Additive).completed += AfterSceneLoad;
        }
    }

    private void AfterSceneLoad(AsyncOperation loadingProcess)
    {
        OnSceneLoaded.Invoke(SceneManager.GetSceneByBuildIndex(_sceneLoadedIndex++).GetRootGameObjects()[0]);
        if (_sceneLoadedIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            OnAllScenesLoaded.Invoke();
        }
    }

}