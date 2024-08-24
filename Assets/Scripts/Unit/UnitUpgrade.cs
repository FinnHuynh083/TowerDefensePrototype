using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitUpgrade : MonoBehaviour
{
    //targetter
    [SerializeField] Targetter _targetter;
    //attack
    [SerializeField] SingleAttack _attack;
    //unit info scriptable obj
    [SerializeField] UnitScriptableObj _unitInfo;

    private int _currentLevel = 0;
    public int CurrentLevel => _currentLevel;

    private void Start()
    {
        SetUpStat();
    }
    private void SetUpStat()
    {
        _targetter._targetRange = _unitInfo._levelInfos[_currentLevel]._attackRange;

        _attack._damage = _unitInfo._levelInfos[_currentLevel]._damage;

        _attack._interval = _unitInfo._levelInfos[_currentLevel]._interval;
    }
    public void UpdateStat()
    {
        if (_currentLevel < _unitInfo._levelInfos.Length-1)
        {
            _currentLevel += 1;

            _targetter._targetRange = _unitInfo._levelInfos[_currentLevel]._attackRange;

            _attack._damage = _unitInfo._levelInfos[_currentLevel]._damage;

            _attack._interval = _unitInfo._levelInfos[_currentLevel]._interval;
        }
    }
}
