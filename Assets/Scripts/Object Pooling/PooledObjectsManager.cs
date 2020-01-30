using UnityEngine;

public class PooledObjectsManager : IPoolManagerCommand<GameObject>
{
    private Transform _pooledObjectsParent;
    private Pool _pool;
    
    public void PopulatePool(IPoolManagerSettings settings, Transform pooledObjectsParent)
    {
        _pooledObjectsParent = pooledObjectsParent;
        _pool = new Pool(settings.Prefab, settings.InitialPoolSize, _pooledObjectsParent);
    }

    public GameObject GetObjectFromPool()
    {
        GameObject obj = _pool.GetObjectFromPool();
        return obj;
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        obj.transform.SetParent(_pooledObjectsParent);
        _pool.ReturnObjectToPool(obj);
    }
}
