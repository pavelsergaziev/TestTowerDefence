using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Раздатчик интерфейсов и "точка входа".
/// </summary>
[DefaultExecutionOrder(-500)]
public class Main : MonoBehaviour
{
    public static Main Instance { get; private set; }

    public event Action OnGameLaunch = delegate { };

    #region settings & interfaces
    [SerializeField] private GameObjectPoolManagerSettings _towersPoolSettings;
    [SerializeField] private GameObjectPoolManagerSettings _enemiesPoolSettings;

    [SerializeField] private TowerSettings[] _towerSettings;
    public ITowerSettings[] TowerSettings { get { return _towerSettings; } }

    [SerializeField] private PlayerStatsSettings _playerStatsSettings;
    public IPlayerStatsSettings PlayerStatsSettings { get { return _playerStatsSettings; } }

    [SerializeField] private EnemyWaveSettings[] _enemyWaves;
    public IEnemyWaveSettings[] EnemyWavesSettings { get { return _enemyWaves; } }

    [SerializeField] private Transform _objectPoolsParent;
    #endregion    

    #region classes & interfaces
    private SceneLoader _sceneLoader;

    public IEnemyPath EnemyPathBuilder { get; private set; }
    public IPlayerBase PlayerBase { get; private set; }

    private PooledObjectsManager _towersPool;
    public IPoolManagerCommand<GameObject> TowersPool { get { return _towersPool; } }

    private PooledObjectsManager _enemiesPool;
    public IPoolManagerCommand<GameObject> EnemiesPool { get { return _enemiesPool; } }    

    private TowerBuildSlotsMenusController _towerBuildSlotsMenusController;
    public ITowerBuildSlotsControllerCommand TowerBuildSlotsMenusControllerCommand { get { return _towerBuildSlotsMenusController; } }
    public ITowerBuildSlotsControllerData TowerBuildSlotsMenusControllerData { get { return _towerBuildSlotsMenusController; } }
    public ITowerBuildSlotsControllerEvents TowerBuildSlotsMenusControllerEvents { get { return _towerBuildSlotsMenusController; } }

    private EnemyWaveController _enemyWaveController;
    public IEnemyWavesControllerEvents EnemyWavesControllerEvents { get { return _enemyWaveController; } }

    private PlayerHealth _playerHealth;
    public IPlayerStatCommand PlayerHealthCommand { get { return _playerHealth; } }
    public IPlayerStatData PlayerHealthData { get { return _playerHealth; } }
    public IPlayerStatEvents PlayerHealthEvents { get { return _playerHealth; } }

    private PlayerMoney _playerMoney;
    public IPlayerStatCommand PlayerMoneyCommand { get { return _playerMoney; } }
    public IPlayerStatData PlayerMoneyData { get { return _playerMoney; } }
    public IPlayerStatEvents PlayerMoneyEvents { get { return _playerMoney; } }

    #endregion

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SetNonMonoBehaviorInterfaces();
        PopulateObjectPools();
    }

    private void Start()
    {
        _sceneLoader = GetComponent<SceneLoader>();
        _sceneLoader.OnSceneLoaded += SetMonoBehaviorInterfaces;
        _sceneLoader.OnAllScenesLoaded += LaunchGame;
        _sceneLoader.LoadGameScenes();
    }

    private void SetNonMonoBehaviorInterfaces()
    {
        _playerHealth = new PlayerHealth();
        _playerMoney = new PlayerMoney();

        _towerBuildSlotsMenusController = new TowerBuildSlotsMenusController();

        _towersPool = new PooledObjectsManager();
        _enemiesPool = new PooledObjectsManager();

        _enemyWaveController = new EnemyWaveController();
    }

    private void PopulateObjectPools()
    {
        _towersPool.PopulatePool(_towersPoolSettings, _objectPoolsParent);
        _enemiesPool.PopulatePool(_enemiesPoolSettings, _objectPoolsParent);
    }

    public void SetMonoBehaviorInterfaces(GameObject rootGameObject)
    {
        if (EnemyPathBuilder == null)
        {
            EnemyPathBuilder = rootGameObject.GetComponentInChildren<IEnemyPath>();
        }

        if (PlayerBase == null)
        {
            PlayerBase = rootGameObject.GetComponentInChildren<IPlayerBase>();
        }
    }

    private void LaunchGame()
    {
        _sceneLoader.OnAllScenesLoaded -= LaunchGame;
        OnGameLaunch.Invoke();
    }
}
