using System.Collections;
using UnityEngine;


public class Tower : MonoBehaviour, ITower
{
    [SerializeField] private LayerMask _enemyLayer;
    public LayerMask EnemyLayer { get { return _enemyLayer; } }
    [SerializeField] private int _enemiesInRangeArrayCount;
        
    private Vector2 _playerBasePosition;

    private float _range;
    public float Range { get { return _range; } }

    private float _shootInterval;
    private int _damage;

    private Transform _transform;    

    private GameObject _currentEnemy;
    private Collider2D[] _enemiesInRangeArray;

    private bool _attackCoroutineIsRunning;
    

    private void Awake()
    {
        _transform = transform;
        _enemiesInRangeArray = new Collider2D[_enemiesInRangeArrayCount];
    }

    public void LoadTowerSettings(ITowerSettings settings)
    {
        _playerBasePosition = Main.Instance.PlayerBase.transform.position;

        GetComponent<SpriteRenderer>().sprite = settings.Sprite;
        _range = settings.ShootRange;
        _shootInterval = settings.ShootInterval;
        _damage = settings.Damage;
    }

    public void ActivateAttackMode()
    {
        if (_attackCoroutineIsRunning)
            return;

        TryReachEnemy();
        StartCoroutine(AttackCoroutine());
    }


    private IEnumerator AttackCoroutine()
    {
        _attackCoroutineIsRunning = true;

        while (_currentEnemy != null)
        {
            Attack();
            yield return new WaitForSeconds(_shootInterval);
            TryReachEnemy();
        }

        _attackCoroutineIsRunning = false;
    }

    private void TryReachEnemy()
    {
        _currentEnemy = null;

        Physics2D.OverlapCircleNonAlloc(_transform.position, _range, _enemiesInRangeArray, _enemyLayer);
        if (_enemiesInRangeArray[0] != null)
        {
            float shortestDistanceFromEnemyToPlayerBase = float.MaxValue;
            float distanceFromCurrentEnemyToPlayerBase = 0;

            foreach (var enemy in _enemiesInRangeArray)
            {
                if (enemy == null)
                    break;

                distanceFromCurrentEnemyToPlayerBase = Vector2.Distance(enemy.transform.position, _playerBasePosition);

                if (distanceFromCurrentEnemyToPlayerBase < shortestDistanceFromEnemyToPlayerBase)
                {
                    shortestDistanceFromEnemyToPlayerBase = distanceFromCurrentEnemyToPlayerBase;
                    _currentEnemy = enemy.gameObject;
                }
            }
        }

        _enemiesInRangeArray.ClearArray();
    }

    private void Attack()
    {
        IDamageableObjectsParent damageableParent = _currentEnemy.GetComponentInParent<IDamageableObjectsParent>();

        if (damageableParent != null)
        {
            damageableParent.DealDamage(_currentEnemy, _damage);
        }
    }
}
