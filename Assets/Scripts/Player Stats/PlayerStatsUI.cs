using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _playerHealthText;
    [SerializeField] private TextMeshProUGUI _playerMoneyText;

    private IPlayerStatData _playerHealthData;
    private IPlayerStatEvents _playerHealthEvents;
    private IPlayerStatData _playerMoneyData;
    private IPlayerStatEvents _playerMoneyEvents;


    private void Start()
    {
        _playerHealthData = Main.Instance.PlayerHealthData;
        _playerHealthEvents = Main.Instance.PlayerHealthEvents;
        _playerMoneyData = Main.Instance.PlayerMoneyData;
        _playerMoneyEvents = Main.Instance.PlayerMoneyEvents;

        ChangeMoneyText(0, _playerMoneyData.CurrentValue);
        ChangeHealthText(0, _playerHealthData.CurrentValue);

        _playerMoneyEvents.OnValueChanged += ChangeMoneyText;
        _playerHealthEvents.OnValueChanged += ChangeHealthText;
    }

    private void ChangeMoneyText(int amountChangedBy, int amountLeftAfterChange)
    {
        _playerMoneyText.text = amountLeftAfterChange.ToString();
    }

    private void ChangeHealthText(int amountChangedBy, int amountLeftAfterChange)
    {
        _playerHealthText.text = amountLeftAfterChange.ToString();
    }

    private void OnDestroy()
    {
        _playerMoneyEvents.OnValueChanged -= ChangeMoneyText;
        _playerHealthEvents.OnValueChanged -= ChangeHealthText;
    }
}
