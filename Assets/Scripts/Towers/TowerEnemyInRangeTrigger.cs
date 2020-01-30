using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class TowerEnemyInRangeTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _towerGameObject;
    private CircleCollider2D _trigger;
    private ITower _tower;

    private void Awake()
    {
        _tower = _towerGameObject.GetComponent<ITower>();

        _trigger = GetComponent<CircleCollider2D>();
        _trigger.isTrigger = true;
    }

    private void OnEnable()
    {
        _trigger.radius = _tower.Range;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (1 << collision.gameObject.layer == _tower.EnemyLayer.value)
        {
            _tower.ActivateAttackMode();
        }
    }
}
