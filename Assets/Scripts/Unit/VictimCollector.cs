using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VictimCollector : MonoBehaviour
{
    [SerializeField] float _delayDestroyTime;

    private float _radius = 0;

    public float Radius
    {
        get => _radius;
        set => _radius = value;
    }

    private void Start()
    {
        transform.localScale *= _radius;
        Destroy(gameObject, _delayDestroyTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
