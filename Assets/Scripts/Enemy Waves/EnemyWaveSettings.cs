using UnityEngine;

[CreateAssetMenu(fileName = "EnemyWave", menuName = "Settings Asset/Enemy Wave")]
public class EnemyWaveSettings : ScriptableObject, IEnemyWaveSettings
{
    [SerializeField] private float _waveDuration;
    public float WaveDuration => _waveDuration;

    [SerializeField] private float _timeIntervalBetweenEnemiesSpawn;
    public float TimeIntervalBetweenEnemiesSpawn => _timeIntervalBetweenEnemiesSpawn;

    [SerializeField] private EnemySettings[] _enemies;
    public IEnemySettings[] Enemies => _enemies as IEnemySettings[];
}
