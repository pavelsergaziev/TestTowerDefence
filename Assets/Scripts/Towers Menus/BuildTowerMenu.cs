using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildTowerMenu : MonoBehaviour
{
    [SerializeField] private GameObject _menuPanel;

    [SerializeField] private Image[] _image;
    [SerializeField] private TextMeshProUGUI[] _priceText;
    [SerializeField] private TextMeshProUGUI[] _rangeText;
    [SerializeField] private TextMeshProUGUI[] _shootIntervalText;
    [SerializeField] private TextMeshProUGUI[] _damageText;
    [SerializeField] private Button[] _buyTowerButton;

    private ITowerBuildSlotsControllerCommand _menuControllerCommand;
    private ITowerBuildSlotsControllerEvents _menuControllerEvents;

    private IPlayerStatData _playerMoneyData;
    private IPlayerStatEvents _playerMoneyEvents;
    private IPlayerStatCommand _playerMoneyCommand;

    private ITowerSettings[] _towers;

    private void Start()
    {
        _towers = new ITowerSettings[4];
        _towers = Main.Instance.TowerSettings;

        _playerMoneyCommand = Main.Instance.PlayerMoneyCommand;
        _playerMoneyData = Main.Instance.PlayerMoneyData;
        _playerMoneyEvents = Main.Instance.PlayerMoneyEvents;

        _menuControllerCommand = Main.Instance.TowerBuildSlotsMenusControllerCommand;
        _menuControllerEvents = Main.Instance.TowerBuildSlotsMenusControllerEvents;

        _menuControllerEvents.OnBuildTowerMenuOpened += OpenMenu;

        FillPanelsInfo();
        ChangeButtonsActiveState();
        _menuPanel.SetActive(false);
    }

    public void OpenMenu()
    {
        ChangeButtonsActiveState();
        _playerMoneyEvents.OnValueChanged += ChangeButtonsActiveState;
        _menuPanel.SetActive(true);
    }
    
    public void CloseMenu()
    {
        _playerMoneyEvents.OnValueChanged -= ChangeButtonsActiveState;
        _menuPanel.SetActive(false);
        _menuControllerCommand.CloseBuildTowerMenu();
    }

    public void BuildTower(int ButtonIndex)
    {
        _playerMoneyCommand.ChangeStatValue(- _towers[ButtonIndex].BuildPrice);
        _menuControllerCommand.BuildTower(_towers[ButtonIndex]);
        CloseMenu();
    }

    private void ChangeButtonsActiveState()
    {
        ChangeButtonsActiveState(0, _playerMoneyData.CurrentValue);
    }

    private void ChangeButtonsActiveState(int moneyAmountChangedBy, int totalMoneyAmount)
    {
        for (int i = 0; i < _buyTowerButton.Length; i++)
        {
            _buyTowerButton[i].interactable = (totalMoneyAmount >= _towers[i].BuildPrice);            
        }
    }

    private void FillPanelsInfo()
    {
        for (int i = 0; i < _towers.Length; i++)
        {
            _image[i].sprite = _towers[i].Sprite;
            _priceText[i].text = _towers[i].BuildPrice.ToString();
            _rangeText[i].text = _towers[i].ShootRange.ToString();
            _shootIntervalText[i].text = _towers[i].ShootInterval.ToString();
            _damageText[i].text = _towers[i].Damage.ToString();
        }
    }

    private void OnDestroy()
    {
        _menuControllerEvents.OnBuildTowerMenuOpened -= OpenMenu;
        _playerMoneyEvents.OnValueChanged -= ChangeButtonsActiveState;
    }
}

