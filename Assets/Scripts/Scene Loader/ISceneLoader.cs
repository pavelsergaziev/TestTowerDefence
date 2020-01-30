using System;
using UnityEngine;

public interface ISceneLoaderCommand
{
    void LoadGameScenes();
}

public interface ISceneLoaderEvents
{
    event Action<GameObject> OnObjectPoolsSceneLoaded;
}