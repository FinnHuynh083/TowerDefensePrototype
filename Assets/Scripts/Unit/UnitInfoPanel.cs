using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UnitInfoPanel : MonoBehaviour
{
    [SerializeField] UnitScriptableObj _unitInfo;
    [SerializeField] float _sellDecreasePercent=0.25f;
    [Header("TMP_Text")]
    [SerializeField] TMP_Text _unitNameText;
    [SerializeField] TMP_Text _sellPriceText;
    [SerializeField] TMP_Text _atkText;
    [SerializeField] TMP_Text _atkSpdText;
    [SerializeField] TMP_Text _upgradePriceText;
    [Header("Game Object")]
    [SerializeField] GameObject _UnitInfoPanel;
    [SerializeField] GameObject _UpgradeButton;
    [SerializeField] GameObject _attackRangeObject;
    [Header("Other")]
    [SerializeField] Toggle _toggle;
    [SerializeField] UnitUpgrade _unitUpgrade;
    public UnityEvent NotEnoughGold;
    public UnityEvent UnitUpgrade;


    //[SerializeField] InputActionReference _mousePos;
    //[SerializeField] InputActionReference _clickAction;
    //[SerializeField] Camera _mainCamera;
    //[SerializeField] LayerMask _unitLayerMask;

    [NonSerialized] public UnitPlacement _unitPlacement;
    [NonSerialized] public Gold _gold;

    private int _currentSellPrice;
    private int _lastLevel=-1;
    private int _currentLevel = 0;

    //set text at start
    private void Start()
    {
        SetInfo();
    }

    private void SetInfo()
    {
        _currentLevel = _unitUpgrade.CurrentLevel;
        var _requiredGold = _unitInfo.TotalGoldValue(_currentLevel);
        //sua lai cong thuc sell price
        _currentSellPrice = Mathf.RoundToInt(_requiredGold * (1 - _sellDecreasePercent));

        _sellPriceText.SetText($"{_currentSellPrice}");
        _unitNameText.SetText($"{_unitInfo._unitName} LV:{_currentLevel + 1}");
        //set atk text
        _atkText.SetText($"Attack: {_unitInfo._levelInfos[_currentLevel]._damage}");
        //set atk spd text
        _atkSpdText.SetText($"Attack SPD: {_unitInfo._levelInfos[_currentLevel]._interval}s/1");
        //set update upgrade price = gold[lv+1]
        if (_currentLevel < _unitInfo._levelInfos.Length-1)
        {
            _upgradePriceText.SetText($"{_unitInfo._levelInfos[_currentLevel + 1]._RequiredGold}");
        }
    }

    private void Update()
    {
        UpdateShow();
        //neu level thay doi >> set lai info
        if(_unitUpgrade.CurrentLevel != _lastLevel)
        {
            SetInfo();
            _lastLevel = _unitUpgrade.CurrentLevel;
        }
    }
    //update unit
    //update stat
    //update model/prefab
    //thay luon prefab khoi thay stat

    //them hien~ thi unit mark doi voi unit dang dc chon

    //show
    public void UpdateShow()
    {
        if (_toggle.isOn)
        {
            _UnitInfoPanel.SetActive(true);

            var radius = _unitInfo._levelInfos[_currentLevel]._attackRange;

            _attackRangeObject.transform.localScale = new Vector3(radius * 2, radius * 2, radius * 2);

            _attackRangeObject.SetActive(true);
        }
        else if(_toggle.isOn==false)
        {
            Hide();
            _attackRangeObject.SetActive(false);
        }
    }
    //hide
    public void Hide()
    {
        _UnitInfoPanel.SetActive(false);
    }

    public void SellUnit()
    {
        _unitPlacement.DestroyUnit(transform.position, gameObject);

        _gold.ChangeBudget(_currentSellPrice);
    }

    public void UpgradeUnit()
    {
        //tien cann
        _currentLevel = _unitUpgrade.CurrentLevel;
        int _requiredGold = _unitInfo._levelInfos[_currentLevel + 1]._RequiredGold;
        if (_gold.IsEnough(_requiredGold))
        {
            _unitUpgrade.UpdateStat();
            _currentLevel += 1;
            _gold.ChangeBudget(-_requiredGold);
            UnitUpgrade.Invoke();
        }
        else
        {
            NotEnoughGold.Invoke();
        }
        //if current lv ==maxlv >> hide nut upgrade
        if (_currentLevel >= _unitInfo._levelInfos.Length-1)
        {
            _UpgradeButton.SetActive(false);
        }
    }
    //if toggle is on >>
    //set sphere scale=radius *2
    //sphere is on ngc lai sphere is off

}
