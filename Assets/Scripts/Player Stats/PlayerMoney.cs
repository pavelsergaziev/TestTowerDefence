using System;

public class PlayerMoney : IPlayerStatCommand, IPlayerStatData, IPlayerStatEvents
{
    public int CurrentValue { get; private set; }
    public event Action<int, int> OnValueChanged = delegate { };


    public PlayerMoney()
    {
        CurrentValue = Main.Instance.PlayerStatsSettings.StartingMoney;
    }

    public void ChangeStatValue(int amountToChangeBy)
    {
        CurrentValue += amountToChangeBy;
        if (CurrentValue <= 0)
        {
            ResetStatValue();
        }

        OnValueChanged.Invoke(amountToChangeBy, CurrentValue);
    }

    public void ResetStatValue()
    {
        CurrentValue = 0;
    }

}
