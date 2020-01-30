using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerHomeBaseEnemiesKilTrigger : MonoBehaviour, IPlayerBase
{
    [SerializeField] private LayerMask _enemiesLayer;

    private Collider2D _trigger;

    private void Awake()
    {
        _trigger = GetComponent<Collider2D>();
        _trigger.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject target = collision.gameObject;

        if (1 << target.layer == _enemiesLayer.value)
        {
            IKillableObjectsGroupParent targetParent = target.GetComponentInParent<IKillableObjectsGroupParent>();
            if (targetParent != null)
            {
                targetParent.Kill(collision.gameObject);
            }            
        }
    }
}
