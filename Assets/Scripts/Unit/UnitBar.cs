using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UnitBar : MonoBehaviour
{
    //[SerializeField] public Toggle[] _unitToggles;
    [SerializeField] private ToggleGroup _togglesGroup;
    [SerializeField] private ToggleGroup _placedToggleGroup;

    [SerializeField] private Toggle[] _toggles;
    [SerializeField] private UnitScriptableObj[] _unitInfos;

    [SerializeField] private UnitGhost unitGhost;
    //[SerializeField] private Gold _gold;
    [SerializeField] private UnitPlacement unitPlacement;

    [SerializeField] private InputActionReference _clickAction;
    [SerializeField] private InputActionReference _mousePos;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private LayerMask _unitToggleLayerMask;

    //public UnityEvent NotEnoughGold;
    //public UnityEvent UnitDataIsNull;

    private Toggle _lastToggle;

    [NonSerialized]public UnitScriptableObj _currentUnitInfo;
    //private UnitLevelInfo _currentUnitLevelInfo;

    private void Start()
    {
        SetTextAndImage();
    }

    private void Update()
    {
        //_currentToggle = _togglesGroup.GetFirstActiveToggle();
        //print($"{ToggleIndex(_currentToggle)}");

        //PlaceUnit();

        UpdateChoosing();

        UpdateChoosingPlacedUnit();

        //PlaceUnit();

    }
    private int ToggleIndex(Toggle toggle)
    {
        int result = -1;
        for(int i = 0; i < _toggles.Length; i++)
        {
            if(toggle== _toggles[i])
            {
                result = i;
                return result;
            }
        }
        return result;
    }

    private void UpdateChoosing()
    {

        if (_togglesGroup.AnyTogglesOn() && _lastToggle != _togglesGroup.GetFirstActiveToggle())
        {
            //destroy last ghost
            unitGhost.DestroyGhostUnit();

            Toggle _currentToggle = _togglesGroup.GetFirstActiveToggle();

            _currentUnitInfo = _unitInfos[ToggleIndex(_currentToggle)];

            //_currentUnitLevelInfo = _currentUnitInfo._levelInfos[_currentUnitInfo.CurrentLevel];
            //spawn ghost unit
            unitGhost.CreateGhostUnit(_currentUnitInfo._unitGhostPrefab);

            //change UnitPlacement prefab
            unitPlacement.unitData = _currentUnitInfo._unitPrefab;
            //
            //unitPlacement._unitInfoData = _currentUnitInfo;

            //new current toggle
            _lastToggle = _currentToggle;
        }
        //neu ko co toggle on>>destroy ghost//unit prefab =null,lasttoggle=null
        if (_togglesGroup.AnyTogglesOn() == false)
        {
            unitGhost.DestroyGhostUnit();
            unitPlacement.unitData = null;
            _currentUnitInfo = null;
            //unitPlacement._unitInfoData = null;

            _lastToggle = null;
        }
    }

    //if enough Gold
    //place unit

    //update choosing & place unit dang bi keu 1 luc

    //start
    //set image & gold
    private void SetTextAndImage()
    {
        for(int i = 0; i < _toggles.Length; i++)
        {
            //_toggles[i].targetGraphic = _unitInfos[i]._unitImage;
            //get parent image
            if (_toggles[i].TryGetComponent<Image>(out Image image))
            {
                image.sprite = _unitInfos[i]._unitSprite;
            }
            var text = _toggles[i].GetComponentInChildren<TMP_Text>();
            //text.SetText($"{_unitInfos[i]._RequiredGold}");

            var _currentInfo = _unitInfos[i]._levelInfos[0];
            text.SetText($"{_currentInfo._RequiredGold}");
        }
    }
    //update gia theo [] info
    private void UpdateChoosingPlacedUnit()
    {
        if (_clickAction.action.triggered)
        {
            Vector2 mousePos = _mousePos.action.ReadValue<Vector2>();

            Ray ray = _mainCamera.ScreenPointToRay((Vector3)mousePos);
            if(Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, _unitToggleLayerMask))
            {

                if (_placedToggleGroup.AnyTogglesOn())
                {
                    _placedToggleGroup.SetAllTogglesOff();
                }
                hitInfo.collider.GetComponent<Toggle>().isOn = true;
            }
        }
    }
    //update unit nao dang dc chon >> toggle on
}
