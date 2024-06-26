using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoving : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _target;
    [SerializeField] private NavMeshAgent _agent;

    private void Update()
    {
        _agent.SetDestination(_target.position);
        _agent.speed = _speed;
    }
}
