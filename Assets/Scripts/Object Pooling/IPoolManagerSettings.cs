using UnityEngine;

public interface IPoolManagerCommand<T>
{
    void PopulatePool(IPoolManagerSettings settings, Transform pooledObjectsParent);
    T GetObjectFromPool();
    void ReturnObjectToPool(T obj);
}

public interface IPoolManagerSettings
{
    GameObject Prefab { get; }
    int InitialPoolSize { get; }    
}
