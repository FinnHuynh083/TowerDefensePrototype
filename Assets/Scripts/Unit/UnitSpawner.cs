using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private InputActionReference _clickAction;

    //[SerializeField] private InputActionReference _mousePos;
    //[SerializeField] private Camera _mainCamera;
    //[SerializeField] private LayerMask layerMask;


    [SerializeField] private Gold _gold;
    [SerializeField] private UnitPlacement unitPlacement;
    [SerializeField] private UnitBar _unitBar;

    public UnityEvent NotEnoughGold;
    public UnityEvent UnitDataIsNull;
    public UnityEvent OutOfGrid;
    public UnityEvent OverLap;

    private UnitScriptableObj _currentUnitInfo;

    private void Update()
    {
        PlaceUnit();
    }
    private void PlaceUnit()
    {
        _currentUnitInfo = _unitBar._currentUnitInfo;
        //if click >> ray cast if ko phai ui >> chay cac lenh place
        if (_clickAction.action.triggered)
        {
            //theem 1 if unit number <= maxnumber
            // return
            //invoke event
            if (_currentUnitInfo == null)
            {
                UnitDataIsNull.Invoke();
                return;
            }
            int _requiredGold = _currentUnitInfo._levelInfos[0]._RequiredGold;
            if (_gold.IsEnough(_requiredGold))
            {
                if (unitPlacement.PlaceUnit() == PlaceStatus.Fits)
                {
                    _gold.ChangeBudget(-_requiredGold);
                }
                else if (unitPlacement.PlaceUnit() == PlaceStatus.OutOfBound)
                {
                    //event
                    OutOfGrid.Invoke();
                }
                else if (unitPlacement.PlaceUnit() == PlaceStatus.Overlaps)
                {
                    //event
                    OverLap.Invoke();
                }
                else
                {
                    //event null data
                    UnitDataIsNull.Invoke();
                }
            }
            else
            {
                NotEnoughGold.Invoke();
            }

            //unitPlacement.PlaceUnit();
        }
    }
}
