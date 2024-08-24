using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class UnitLevelInfo
{
    public int _RequiredGold;
    //public GameObject _unitPrefab;
    //public GameObject _unitGhostPrefab;
    //dame
    public float _damage;
    //interval
    public float _interval;
    //range >> hien thi them range tren unit info panel
    public float _attackRange;
}

[CreateAssetMenu(fileName ="Unit Info",menuName ="Unit/Unit Info",order =3)]
public class UnitScriptableObj : ScriptableObject
{
    //public int _RequiredGold;

    public GameObject _unitPrefab;
    public GameObject _unitGhostPrefab;
    public String _unitName;
    public Sprite _unitSprite;

    public UnitLevelInfo[] _levelInfos;

    //public int CurrentLevel=0;
    //public int MaxLevel;
    public float TotalGoldValue(int level)
    {
        float result=0;
        for(int i=0;i<= level; i++)
        {
            result += _levelInfos[i]._RequiredGold;
        }
        return result;
    }


}
