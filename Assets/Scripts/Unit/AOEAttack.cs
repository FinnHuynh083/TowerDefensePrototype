using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class AOEAttack : SingleAttack
{
    [SerializeField] private GameObject _victimCollectorPrefab;
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _enemyLayerMask;

    private Collider[] Victims;

    private Collider[] CollectVictim()

    {
        var _target = _targetter.CurrentTarget;

        if (_target == null) return null;

        GameObject _victimCollector = Instantiate(_victimCollectorPrefab, _target.Position, _target.Rotation);

        _victimCollector.GetComponent<VictimCollector>().Radius = _radius;

        return Physics.OverlapSphere(_victimCollector.transform.position, _radius, _enemyLayerMask);

    }

    public void TriggerEnemyCollector()//doi thanh trigger collector
    {
        //sinh Victim Collector tai CurrentTarget.Pos tai luc AttackNow
        Victims = CollectVictim();

        foreach (var victim in Victims)
        {
            print(victim.name);
        }
    }


    //Trigger VFX

    //Trigger Atk
    public override void TriggerAttack()
    {
        foreach (var victim in Victims)
        {
            victim.GetComponent<Targetable>().Health.TakeDamage(_damage);
        }
    }

}
