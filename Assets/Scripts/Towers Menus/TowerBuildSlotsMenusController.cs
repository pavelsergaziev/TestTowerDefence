using System;

public class TowerBuildSlotsMenusController : ITowerBuildSlotsControllerCommand, ITowerBuildSlotsControllerEvents, ITowerBuildSlotsControllerData
{
    public ITowerSettings LastBuiltTowerSettings { get; private set; }

    public event Action OnBuildTowerMenuOpened = delegate { };
    public event Action<int> OnSellTowerMenuOpened = delegate { };
    public event Action OnBuildTowerMenuClosed = delegate { };
    public event Action OnSellTowerMenuClosed = delegate { };
    public event Action<ITowerSettings> OnBuildTower = delegate { };
    public event Action OnSellTower = delegate { };

    public TowerBuildSlotsMenusController()
    {
        LastBuiltTowerSettings = Main.Instance.TowerSettings[0];
    }

    public void BuildTower(ITowerSettings towerSettings)
    {
        LastBuiltTowerSettings = towerSettings;
        OnBuildTower.Invoke(towerSettings);        
    }

    public void OpenBuildTowerMenu()
    {
        OnBuildTowerMenuOpened.Invoke();
    }

    public void OpenSellTowerMenu(int sellPrice)
    {
        OnSellTowerMenuOpened.Invoke(sellPrice);
    }

    public void SellTower()
    {
        OnSellTower.Invoke();
    }

    public void CloseBuildTowerMenu()
    {
        OnBuildTowerMenuClosed.Invoke();
    }

    public void CloseSellTowerMenu()
    {
        OnSellTowerMenuClosed.Invoke();
    }
}
    
