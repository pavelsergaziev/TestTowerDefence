
public interface IEnemyWaveSettings
{
    float WaveDuration { get; }
    float TimeIntervalBetweenEnemiesSpawn { get; }
    IEnemySettings[] Enemies { get; }
}
