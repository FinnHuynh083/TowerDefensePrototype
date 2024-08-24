using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DoTweenTest : MonoBehaviour
{
    [SerializeField] private Transform _uIObject;
    [SerializeField] private float _startX;

    private void Start()
    {
        var seq = DOTween.Sequence();

        var action = _uIObject.DOMoveX(_uIObject.position.x, 1).From(_startX);

        seq.Append(action);
        //seq.Append(_uIObject.DOMoveX(_uIObject.position.x, 1).From(_startX));
    }
}
