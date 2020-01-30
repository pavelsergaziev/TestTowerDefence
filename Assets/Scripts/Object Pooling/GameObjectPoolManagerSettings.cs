using UnityEngine;

[CreateAssetMenu(fileName = "PoolManagerSettings", menuName = "Settings Asset/Pool Manager")]
public class GameObjectPoolManagerSettings : ScriptableObject, IPoolManagerSettings
{
    [SerializeField] private GameObject _prefab;
    public GameObject Prefab => _prefab;

    [SerializeField] private int _initialPoolSize;
    public int InitialPoolSize => _initialPoolSize;
}
