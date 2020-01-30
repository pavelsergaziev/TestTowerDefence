using UnityEngine;

public interface IEnemySettings
{
    Sprite Sprite { get; }
    float Speed { get; }
    int Health { get; }
    int Damage { get; }
    int MoneyDroppedOnDeathMin { get; }
    int MoneyDroppedOnDeathMax { get; }
}