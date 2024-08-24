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
    [SerializeField] private GameObject _atkVFXPrefab;
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _enemyLayerMask;

    private Collider[] Victims;

    private Collider[] CollectVictim()

    {
        var _target = _targetter.CurrentTarget;

        if (_target == null) return null;

        GameObject _victimCollector = Instantiate(_victimCollectorPrefab, _target.Position, _target.Rotation);

        _victimCollector.GetComponent<AOEVFX>().Radius = _radius;

        return Physics.OverlapSphere(_victimCollector.transform.position, _radius, _enemyLayerMask);

    }

    public void TriggerEnemyCollector()//doi thanh trigger collector
    {
        //sinh Victim Collector tai CurrentTarget.Pos tai luc AttackNow
        Victims = CollectVictim();

        //foreach (var victim in Victims)
        //{
        //    print(victim.name);
        //}
    }

    private Collider AvailableVictim()
    {
        foreach(var v in Victims)
        {
            if (v != null)
            {
                return v;
            }
        }
        return null;
    }

    //Trigger VFX
    public override void TriggerVFX()
    {
        //sua thanh neu ko co target >> lay target tiep theo
        //var _target = _targetter.CurrentTarget;

        var _target = AvailableVictim().transform;
        if (_target != null)
        {
            GameObject _atkVFX = Instantiate(_atkVFXPrefab, _target.position, _target.rotation);

            _atkVFX.GetComponent<AOEVFX>().Radius = _radius;
        }
    }
    //Trigger Atk
    public override void TriggerAttack()
    {
        foreach (var victim in Victims)
        {
            //victim.GetComponent<Targetable>().Health.TakeDamage(_damage);
            //kiem tra neu ko con collider >>> next
            if (victim != null)
            {
                if (victim.TryGetComponent<Targetable>(out Targetable component))
                {
                    component.Health.TakeDamage(_damage);
                }
            }
        }
    }

}
