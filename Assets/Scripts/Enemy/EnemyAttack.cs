using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : SingleAttack
{
    [SerializeField] public GameObject _target;

    public override void TriggerAttack()
    {
        Health _targetHealth = _target.GetComponent<Health>();
        _targetHealth.TakeDamage(_damage);
    }
}
