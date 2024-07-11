using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoving : MonoBehaviour
{
    [SerializeField] public float _speed;
    [SerializeField] public Transform _target;
    [SerializeField] public NavMeshAgent _agent;

    private void Update()
    {
        _agent.SetDestination(_target.position);
        _agent.speed = _speed;
    }

    public bool IsArrived()
    {
        if (Vector3.Distance(transform.position, _target.position) <= _agent.stoppingDistance)
        {
            return true;
        }
        return false;
    }
}
