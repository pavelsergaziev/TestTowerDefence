using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "Settings Asset/Player")]
public class PlayerStatsSettings : ScriptableObject, IPlayerStatsSettings
{
    [SerializeField] public int _maxHealth;
    public int MaxHealth => _maxHealth;

    [SerializeField] public int _startingMoney;
    public int StartingMoney => _startingMoney;
}
