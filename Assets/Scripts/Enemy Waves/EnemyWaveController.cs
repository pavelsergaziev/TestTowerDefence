using System;
using System.Collections;
using UnityEngine;

public class EnemyWaveController : IEnemyWavesControllerEvents
{
    private IEnemyWaveSettings[] _waves;

    private int _currentWaveIndex;
    private int _nextEnemyIndex;


    public event Action<IEnemySettings> OnEnemySpawn = delegate { };

    public EnemyWaveController()
    {
        _waves = Main.Instance.EnemyWavesSettings;
        Main.Instance.OnGameLaunch += BeginWaves;
    }

    public void BeginWaves()
    {
        Main.Instance.StartCoroutine(WavesSwitchCoroutine(0));
    }

    private IEnumerator EnemiesSpawnCoroutine(int waveIndex)
    {
        WaitForSeconds delay = new WaitForSeconds(_waves[waveIndex].TimeIntervalBetweenEnemiesSpawn);

        int nextEnemyIndex = 0;

        while (nextEnemyIndex < _waves[waveIndex].Enemies.Length)
        {
            OnEnemySpawn.Invoke(_waves[waveIndex].Enemies[nextEnemyIndex++]);
            yield return delay;
        }
    }

    private IEnumerator WavesSwitchCoroutine(int waveIndex)
    {
        WaitForSeconds delay = new WaitForSeconds(_waves[waveIndex].WaveDuration);

        Main.Instance.StartCoroutine(EnemiesSpawnCoroutine(waveIndex));
        yield return delay;

        if (++waveIndex < _waves.Length)
        {
            Main.Instance.StartCoroutine(WavesSwitchCoroutine(waveIndex));
        }        
    }
}
