using System;

public class PlayerHealth : IPlayerStatCommand, IPlayerStatData, IPlayerStatEvents
{    
    public int CurrentValue { get; private set; }
    public event Action<int, int> OnValueChanged = delegate { };

    private int _maxHealth;


    public PlayerHealth()
    {
        _maxHealth = Main.Instance.PlayerStatsSettings.MaxHealth;
        CurrentValue = _maxHealth;
    }

    public void ChangeStatValue(int amountToChangeBy)
    {
        CurrentValue += amountToChangeBy;
        if (CurrentValue < 0)
        {
            CurrentValue = 0;
        }
        else if (CurrentValue > _maxHealth)
        {
            ResetStatValue();
        }

        OnValueChanged.Invoke(amountToChangeBy, CurrentValue);
    }

    public void ResetStatValue()
    {
        CurrentValue = _maxHealth;
    }
}