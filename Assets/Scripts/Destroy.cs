using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    [SerializeField] private float _delayTime;

    public void DestroyGameObject() => Destroy(gameObject, _delayTime);
}
