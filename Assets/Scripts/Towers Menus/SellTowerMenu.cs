using UnityEngine;
using TMPro;

public class SellTowerMenu : MonoBehaviour
{
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private TextMeshProUGUI _sellPriceText;

    private ITowerBuildSlotsControllerCommand _menuControllerCommand;
    private ITowerBuildSlotsControllerEvents _menuControllerEvents;

    private IPlayerStatData _playerMoneyData;
    private IPlayerStatEvents _playerMoneyEvents;
    private IPlayerStatCommand _playerMoneyCommand;

    private int _sellPrice;

    private void Start()
    {
        _playerMoneyCommand = Main.Instance.PlayerMoneyCommand;
        _playerMoneyData = Main.Instance.PlayerMoneyData;
        _playerMoneyEvents = Main.Instance.PlayerMoneyEvents;

        _menuControllerCommand = Main.Instance.TowerBuildSlotsMenusControllerCommand;
        _menuControllerEvents = Main.Instance.TowerBuildSlotsMenusControllerEvents;

        _menuControllerEvents.OnSellTowerMenuOpened += OpenMenu;

        _menuPanel.SetActive(false);
    }    

    private void OpenMenu(int sellPrice)
    {
        _sellPrice = sellPrice;
        FillPanelsInfo();
        _menuPanel.SetActive(true);
    }

    private void FillPanelsInfo()
    {
        _sellPriceText.text = _sellPrice.ToString();
    }

    public void CloseMenu()
    {
        _menuPanel.SetActive(false);
        _menuControllerCommand.CloseSellTowerMenu();
    }

    public void SellTower()
    {
        _playerMoneyCommand.ChangeStatValue(_sellPrice);
        _menuControllerCommand.SellTower();
        CloseMenu();
    }

    private void OnDestroy()
    {
        _menuControllerEvents.OnSellTowerMenuOpened -= OpenMenu;
    }
}
