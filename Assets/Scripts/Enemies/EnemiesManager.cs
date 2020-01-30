using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour, IDamageableObjectsParent, IKillableObjectsGroupParent
{
    [SerializeField] private float _minDistanceToWayPoint = 0.01f;

    private IPoolManagerCommand<GameObject> _pool;
    private IEnemyPath _pathbuilder;

    private IPlayerStatCommand _playerMoney;
    private IPlayerStatCommand _playerHealth;

    private IEnemyWavesControllerEvents _enemyWavesController;

    private Vector3[] _path;

    private Dictionary<GameObject, Enemy> _enemies;
    private float _travelDistance;
    private float _distanceToNextWaypoint;
    private int _waypointIndex;    

    private class Enemy
    {
        public Transform Transform;
        public float Speed;
        public int Health;
        public int Damage;
        public int MoneyDroppedOnDeathMin;
        public int MoneyDroppedOnDeathMax;
        public int NextWaypointIndex;
    }

    private void Start()
    {
        _pathbuilder = Main.Instance.EnemyPathBuilder;
        _pool = Main.Instance.EnemiesPool;
        _playerMoney = Main.Instance.PlayerMoneyCommand;
        _playerHealth = Main.Instance.PlayerHealthCommand;
        _enemyWavesController = Main.Instance.EnemyWavesControllerEvents;

        _enemies = new Dictionary<GameObject, Enemy>();
        _path = _pathbuilder.GetPath();

        _enemyWavesController.OnEnemySpawn += SpawnEnemy;
    }

    public void DealDamage(GameObject damageReceiver, int damageAmount)
    {
        _enemies[damageReceiver].Health -= damageAmount;
        if (_enemies[damageReceiver].Health <= 0)
        {
            KillEnemy(damageReceiver);
        }
    }

    public void Kill(GameObject objectToKill)
    {
        KillEnemy(objectToKill, true);
    }

    private void KillEnemy(GameObject killedEnemy, bool enemyReachedPlayerHomeBase=false)
    {
        if (!enemyReachedPlayerHomeBase)
        {
            int moneyDroppedMin = _enemies[killedEnemy].MoneyDroppedOnDeathMin;
            int moneyDroppedMax = _enemies[killedEnemy].MoneyDroppedOnDeathMax;
            _playerMoney.ChangeStatValue(UnityEngine.Random.Range(moneyDroppedMin, moneyDroppedMax + 1));
        }
        else
        {
            _playerHealth.ChangeStatValue(- _enemies[killedEnemy].Damage);
        }

        _enemies.Remove(killedEnemy);
        _pool.ReturnObjectToPool(killedEnemy);
        killedEnemy.SetActive(false);        
    }

    private void SpawnEnemy(IEnemySettings enemySettings)
    {
        GameObject enemyGameObject = _pool.GetObjectFromPool();
        Enemy enemy = new Enemy();

        enemyGameObject.GetComponent<SpriteRenderer>().sprite = enemySettings.Sprite;
        enemy.Transform = enemyGameObject.transform;
        enemy.Damage = enemySettings.Damage;
        enemy.Speed = enemySettings.Speed;
        enemy.Health = enemySettings.Health;
        enemy.MoneyDroppedOnDeathMin = enemySettings.MoneyDroppedOnDeathMin;
        enemy.MoneyDroppedOnDeathMax = enemySettings.MoneyDroppedOnDeathMax;

        enemy.Transform.SetParent(transform);
        enemy.Transform.position = transform.position;

        enemyGameObject.SetActive(true);

        _enemies.Add(enemyGameObject, enemy);
    }

    private void Update()
    {
        foreach (var enemy in _enemies)
        {
            _waypointIndex = enemy.Value.NextWaypointIndex;

            _travelDistance = enemy.Value.Speed * Time.deltaTime;
            Vector3 objectPosition = enemy.Value.Transform.position;

            _distanceToNextWaypoint = Vector3.Distance(enemy.Value.Transform.position, _path[_waypointIndex]);

            while (_distanceToNextWaypoint < _minDistanceToWayPoint && _waypointIndex < _path.Length - 1)
            {
                _distanceToNextWaypoint = Vector3.Distance(enemy.Value.Transform.position, _path[++_waypointIndex]);
            }

            float currentDistance = _travelDistance;

            while (currentDistance >= _distanceToNextWaypoint && _waypointIndex < _path.Length - 2)
            {
                currentDistance -= _distanceToNextWaypoint;
                objectPosition = _path[_waypointIndex];
                _distanceToNextWaypoint = Vector3.Distance(_path[_waypointIndex], _path[++_waypointIndex]);
            }

            enemy.Value.Transform.position = Vector3.MoveTowards(objectPosition, _path[_waypointIndex], currentDistance);

            enemy.Value.NextWaypointIndex = _waypointIndex;
        }
    }

    private void OnDestroy()
    {
        _enemyWavesController.OnEnemySpawn -= SpawnEnemy;
    }
}
