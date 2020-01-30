using UnityEngine;

public class Selector : MonoBehaviour
{
    private ITowerBuildSlotsControllerEvents _menuEvents;

    private Camera _camera;
    private RaycastHit2D[] _hitResult;

    private bool _canSelect;

    private void Awake()
    {
        _camera = Camera.main;
        _hitResult = new RaycastHit2D[1];
    }

    private void Start()
    {
        _menuEvents = Main.Instance.TowerBuildSlotsMenusControllerEvents;

        _menuEvents.OnBuildTowerMenuOpened += TurnGameObjectSelectorOff;
        _menuEvents.OnSellTowerMenuOpened += TurnGameObjectSelectorOff;
        _menuEvents.OnSellTowerMenuClosed += TurnGameObjectSelectorOn;
        _menuEvents.OnBuildTowerMenuClosed += TurnGameObjectSelectorOn;

        TurnGameObjectSelectorOn();
    }

    private void TurnGameObjectSelectorOff(int uselessArg)
    {
        TurnGameObjectSelectorOff();
    }

    private void TurnGameObjectSelectorOff()
    {
        _canSelect = false;
    }

    private void TurnGameObjectSelectorOn()
    {
        _canSelect = true;
    }

    private void Update()
    {
        if (!_canSelect)
            return;

#if UNITY_STANDALONE
        if (Input.GetMouseButtonDown(0))
        {
            Physics2D.RaycastNonAlloc(_camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, _hitResult);
            Select(_hitResult[0].transform);
            _hitResult[0] = default;
        }
#endif

#if UNITY_ANDROID
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Physics2D.RaycastNonAlloc(_camera.ScreenToWorldPoint(Input.GetTouch(0).position), Vector2.zero, _hitResult);
            Select(_hitResult[0].transform);
            _hitResult[0] = default;
        }
#endif
    }

    public void Select(Transform selectableObjectTransform)
    {
        if (selectableObjectTransform == null)
            return;


        ISelectable selectableObject = selectableObjectTransform.GetComponentInParent<ISelectable>();

        if (selectableObject != null)
        {
            selectableObject.HandleSelect();
        }
    }

    private void OnDestroy()
    {
        _menuEvents.OnBuildTowerMenuOpened -= TurnGameObjectSelectorOff;
        _menuEvents.OnSellTowerMenuOpened -= TurnGameObjectSelectorOff;
        _menuEvents.OnSellTowerMenuClosed -= TurnGameObjectSelectorOn;
        _menuEvents.OnBuildTowerMenuClosed -= TurnGameObjectSelectorOn;
    }
}
