using UnityEngine;

public interface ITower
{
    void LoadTowerSettings(ITowerSettings settings);

    void ActivateAttackMode();
    float Range { get; }
    LayerMask EnemyLayer { get; }
}
