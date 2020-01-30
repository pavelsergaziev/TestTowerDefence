using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    private Queue<GameObject> _pool;
    private GameObject _prefab;
    private Transform _parent;

    public Pool(GameObject prefab, int initialPoolSize, Transform objectsParent)
    {
        _prefab = prefab;
        _parent = objectsParent;
        _pool = new Queue<GameObject>(initialPoolSize);

        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject obj = InstantiatePrefab();
            obj.SetActive(false);
            _pool.Enqueue(obj);
        }
    }

    private GameObject InstantiatePrefab()
    {
        return Object.Instantiate(_prefab, _parent);
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        _pool.Enqueue(obj);
    }

    public GameObject GetObjectFromPool()
    {
        GameObject obj;
        if (_pool.Count > 0)
        {
            obj = _pool.Dequeue();
        }
        else
        {
            obj = InstantiatePrefab().gameObject;
        }

        return obj;
    }
}
