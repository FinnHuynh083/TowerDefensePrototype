using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCurve : MonoBehaviour
{
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private float _speed;
    //[SerializeField] private float _pathHeight; dung curve

    private Vector3 _departure;
    private Vector3 _destination;
    private float xDistance;
    private float TotalXDistance;
    //dung toan hoc - st goc ban mac dinh 45 do
    private float _pathHeight;

    public void SetTarget(Transform target)
    {
        _destination = target.position;
        TotalXDistance = Vector3.Distance(transform.position, _destination);
        _pathHeight = TotalXDistance / 2;
        //chiu cao  = quang duong/2 >> goc ban mac dinh =45 do
    }

    private void Start()
    {
        _departure = transform.position;
        xDistance = 0;
    }
    private void Update()
    {
        xDistance += Time.deltaTime * _speed;
        var fraction = xDistance / TotalXDistance;
        var height = _curve.Evaluate(fraction);
        transform.position = Vector3.Lerp(_departure, _destination, fraction);
        transform.position += Vector3.up * height * _pathHeight;
    }
}
