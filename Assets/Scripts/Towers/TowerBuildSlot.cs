using UnityEngine;

public class TowerBuildSlot : MonoBehaviour, ISelectable
{
    [SerializeField] private GameObject _slot;
    private GameObject _tower;
    private IPoolManagerCommand<GameObject> _towersPool;
    private ITowerBuildSlotsControllerEvents _menuEvents;
    private ITowerBuildSlotsControllerCommand _menuCommand;

    private int _currentBuiltTowerPrice;

    public static TowerBuildSlot CurrentlySelectedSlot {get; private set;}

    private void Start()
    {
        _towersPool = Main.Instance.TowersPool;
        _menuEvents = Main.Instance.TowerBuildSlotsMenusControllerEvents;
        _menuCommand = Main.Instance.TowerBuildSlotsMenusControllerCommand;

        _menuEvents.OnBuildTower += BuildTower;
        _menuEvents.OnSellTower += SellTower;
    }

    public void HandleSelect()
    {
        CurrentlySelectedSlot = this;

        if (_tower == null)
        {
            _menuCommand.OpenBuildTowerMenu();
        }
        else
        {
            _menuCommand.OpenSellTowerMenu(_currentBuiltTowerPrice);
        }
    }

    private void BuildTower(ITowerSettings towerSettings)
    {
        if (!IsSelected())
            return;

        _slot.SetActive(false);
        _tower = _towersPool.GetObjectFromPool();
        _tower.transform.SetParent(transform);
        _tower.transform.position = transform.position;

        _tower.GetComponent<ITower>().LoadTowerSettings(towerSettings);

        _currentBuiltTowerPrice = towerSettings.BuildPrice;

        _tower.SetActive(true);
    }

    private void SellTower()
    {
        if (!IsSelected())
            return;

        _tower.SetActive(false);
        _towersPool.ReturnObjectToPool(_tower);
        _tower = null;
        _slot.SetActive(true);
    }

    private bool IsSelected()
    {
        if (CurrentlySelectedSlot != this)
            return false;

        CurrentlySelectedSlot = null;
        return true;
    }

    private void OnDestroy()
    {
        _menuEvents.OnBuildTower -= BuildTower;
        _menuEvents.OnSellTower -= SellTower;
    }
}
