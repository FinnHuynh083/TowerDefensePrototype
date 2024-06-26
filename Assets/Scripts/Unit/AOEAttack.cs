using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEAttack : SingleAttack
{
    [SerializeField] private GameObject _victimCollectorPrefab;
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _enemyLayerMask;

    public override void TriggerAttack()
    {
        //sinh Victim Collector tai CurrentTarget.Pos tai luc AttackNow
        Collider[] Victims= CollectVictim();
        foreach(var victim in Victims)
        {
            print(victim.name);
        }
    }

    private Collider[] CollectVictim()
    {
        var _target = _targetter.CurrentTarget;

        GameObject _victimCollector = Instantiate(_victimCollectorPrefab, _target.Position, _target.Rotation);

        return Physics.OverlapSphere(_victimCollector.transform.position, _radius, _enemyLayerMask);
    }

    //Create VFX

    //Trigger VFX
}
