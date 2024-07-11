using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Targetable : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Health _health;

    public float RemainingDistance => _agent.remainingDistance;

    public Transform Transform => gameObject.transform;

    public Vector3 Position => gameObject.transform.position;

    public Quaternion Rotation => gameObject.transform.rotation;

    public NavMeshAgent Agent => _agent;

    public Health Health => _health;

    //public bool IsDead => _health.IsDead;
    public bool IsDead
    {
        get
        {
            if (_health != null)
            {
                return _health.IsDead;
            }
            return false;
        }
    }

}
