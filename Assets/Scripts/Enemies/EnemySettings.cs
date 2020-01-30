using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Settings Asset/Enemy")]
public class EnemySettings : ScriptableObject, IEnemySettings
{
    [SerializeField] private Sprite _sprite;
    public Sprite Sprite => _sprite;

    [SerializeField] private float _speed;
    public float Speed => _speed;

    [SerializeField] private int _health;
    public int Health => _health;

    [SerializeField] private int _damage;
    public int Damage => _damage;

    [SerializeField] private int _moneyDroppedOnDeathMin;
    public int MoneyDroppedOnDeathMin => _moneyDroppedOnDeathMin;

    [SerializeField] private int _moneyDroppedOnDeathMax;
    public int MoneyDroppedOnDeathMax => _moneyDroppedOnDeathMax;    
}
