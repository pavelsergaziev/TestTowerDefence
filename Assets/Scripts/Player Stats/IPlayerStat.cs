using System;
using UnityEngine;

public interface IPlayerStatCommand
{
    void ChangeStatValue(int amountToChangeBy);
    void ResetStatValue();
}

public interface IPlayerStatData
{
    int CurrentValue { get; }
}

public interface IPlayerStatEvents
{
    /// <summary>
    /// Насколько изменилось значение / Итоговое значение
    /// </summary>
    event Action<int, int> OnValueChanged;
}
