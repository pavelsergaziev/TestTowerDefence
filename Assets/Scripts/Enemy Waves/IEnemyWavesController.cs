using System;

public interface IEnemyWavesControllerEvents
{
    event Action<IEnemySettings> OnEnemySpawn;
}
