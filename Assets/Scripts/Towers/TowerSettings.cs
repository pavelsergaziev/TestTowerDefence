using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerSettings", menuName = "Settings Asset/Tower")]
public class TowerSettings : ScriptableObject, ITowerSettings
{
    [SerializeField] private Sprite _sprite;
    public Sprite Sprite => _sprite;

    [SerializeField] private int _buildPrice;
    public int BuildPrice => _buildPrice;

    [SerializeField] private float _shootRange;
    public float ShootRange => _shootRange;

    [SerializeField] private float _shootInterval;
    public float ShootInterval => _shootInterval;

    [SerializeField] private int _damage;
    public int Damage => _damage;
}
