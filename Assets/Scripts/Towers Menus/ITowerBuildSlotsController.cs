using System;

public interface ITowerBuildSlotsControllerData
{
    ITowerSettings LastBuiltTowerSettings { get; }
}

public interface ITowerBuildSlotsControllerCommand
{
    void OpenBuildTowerMenu();
    void OpenSellTowerMenu(int sellPrice);
    void BuildTower(ITowerSettings towerSettings);
    void SellTower();
    void CloseBuildTowerMenu();
    void CloseSellTowerMenu();
}

public interface ITowerBuildSlotsControllerEvents
{    
    /// <summary>
    /// цена продажи
    /// </summary>
    event Action<int> OnSellTowerMenuOpened;
    event Action OnSellTowerMenuClosed;
    event Action OnSellTower;

    event Action OnBuildTowerMenuOpened;
    event Action OnBuildTowerMenuClosed;    
    event Action<ITowerSettings> OnBuildTower;    
}