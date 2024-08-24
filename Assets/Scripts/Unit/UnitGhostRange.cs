using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGhostRange : MonoBehaviour
{
    [SerializeField] private float _range;
    [SerializeField] private GameObject _rangeObject;

    private void Start()
    {
        float _trueRange = _range * 2;
        _rangeObject.transform.localScale =new Vector3(_trueRange, _trueRange, _trueRange);
    }
}
